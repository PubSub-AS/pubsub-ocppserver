
using PubSub.OcppServer.Models.FramingProtocol;
using PubSub.OcppServer.Models.Internal;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using PubSub.OcppServer.Data.Interfaces;
using ResetTypeEnum = PubSub.OcppServer.Models.Internal.ResetTypeEnum;
using PubSub.OcppServer.Services.Interfaces;
using PubSub.OcppServer.Models.Ocpp;
using PubSub.OcppServer.Models.Dtos;

namespace PubSub.OcppServer.Services
{
    public abstract class OcppHandler : IOcppHandler
    {
        protected readonly IOcppMessageSerializer _ocppMessageSerializer;
        protected readonly ILogger<IOcppHandler> _logger;

        protected readonly IOcppServer _ocppServer;
        protected readonly IOcppClientManager _websocketManager;
        protected readonly IUnitOfWork _unitOfWork;
        protected OcppHandlerContext _context;
        protected readonly IOcppRequestManager _ocppRequestManager;
        protected readonly IChargingProfileService _chargingProfileService;
        private readonly IOcppMessageSerializer _messageSerializer;
        private readonly ISendMessageBus _sendMessageBus;
        protected OcppVersionEnum _ocppVersion;
        protected Dictionary<string, object> _messageHandlers;



        public OcppHandler(IOcppMessageSerializer messageSerializer,
            ILogger<IOcppHandler> logger,
            IOcppServer ocppServer,
            IOcppClientManager webSocketManager,
            IUnitOfWork unitOfWork,
            OcppHandlerContext context,
            IOcppRequestManager ocppRequestManager,
            IChargingProfileService chargingProfileService,
            IOcppMessageSerializer ocppMessageSerializer,
            ISendMessageBus sendMessageBus)
        {
            _context = context;
            _ocppMessageSerializer = messageSerializer;
            _logger = logger;
            _ocppServer = ocppServer;
            _websocketManager = webSocketManager;
            _unitOfWork = unitOfWork;
            _ocppRequestManager = ocppRequestManager;
            _chargingProfileService = chargingProfileService;
            _messageSerializer = ocppMessageSerializer;
            _sendMessageBus = sendMessageBus;
        }

        public string ChargingPointId 
        {
            get => _context.ChargingPointId;
            set => _context.ChargingPointId = value;
        }

        public OcppVersionEnum OcppVersion => _ocppVersion;


        public virtual void HandleIncomingRequest(string rawMessage)
        {
            var call = new Call(rawMessage);
            if (call.Action == null)
            {
                _logger.LogCritical("Received Call without Action");
                return;
            }
            if (!_messageHandlers.TryGetValue(call.Action.Trim('"'), out var handler))
            {
                // Unsupported message type
                var callError = new CallError(call, "NotSupported", $"Message type {call.Action} is not supported.", null);
                var serializedCallError = _messageSerializer.SerializeCallError(callError.UniqueId, callError.ErrorCode, callError.ErrorDescription);
                _sendMessageBus.Add(serializedCallError);
                return;
            }

            // Deserialize payload
            if (call.Payload == null)
            {
                _logger.LogCritical("Tried to dispatch msg without payload");
                return;
            }
            var payload = call.Payload.ToString();
            var handlerType = handler.GetType().GetInterfaces().FirstOrDefault(i => i.Name.StartsWith("IOcppMessageIncomingHandler"));
            if (handlerType != null)
            {
                var requestType = handlerType.GetGenericArguments()[0];
                var request = _messageSerializer.DeserializeRequest(payload, requestType);

                // Invoke the handler and get the response
                var method = handler.GetType().GetMethod("Handle");
                if (method == null)
                {
                    _logger.LogCritical("Tried to dispatch msg without a Handle method");
                }
                var response = method.Invoke(handler, new[] { request });

                // Serialize the response and send it back to the charging point
                var serializedResponse = _messageSerializer.SerializeCallResult(call.UniqueId, response);
                _sendMessageBus.Add(serializedResponse);

                _logger.LogInformation($"Handled {call.Action} message");
            }
            else
            {
                _logger.LogWarning($"Handler not found for action {call.Action}");
            }
        }


        protected async Task<OcppResponseOrError> ParseAndSendOcppRequest<TRequest, TResponse>(string ocppAction, TRequest payload)
            where TRequest : IOcppRequest
        {
            var call = new Call
            {
                Action = ocppAction,
                MessageTypeId = (int)MessageType.CALL,
                Payload = payload,
                UniqueId = Guid.NewGuid().ToString()
            };
            var requestFrameMessage = _ocppMessageSerializer
                .SerializeCall(call);
            var ocppClientTask = await _ocppRequestManager.CreateAndSendPendingRequest(call, requestFrameMessage, typeof(TResponse));
            return ocppClientTask;
        }

        public abstract Task<ApiResponseDto> CancelReservationAsync(int reservationId);
        public abstract Task<ApiResponseDto> ChangeConfigurationAsync(string configKey, string configValue);
        public abstract Task<ApiResponseDto> ClearCacheAsync();
        public abstract Task<ApiResponseDto> GetConfigurationAsync(List<string> configKeys);
        public abstract Task<ApiResponseDto> ResetChargingPointAsync(bool isHard);
        public abstract Task<ApiResponseDto> ReserveNowAsync(int connectorId, DateTimeOffset expiryDate, string idTag);
        public abstract Task<ApiResponseDto> RemoteStartTransactionAsync(int connectorId, string idTag);
        public abstract Task<ApiResponseDto> RemoteOptimizedStartTransactionAsync(int connectorId, string idTag, OptimizedChargingArgs optimizedChargingArgs);
        public abstract Task<ApiResponseDto> RemoteStopTransactionAsync(int transactionId);
        public abstract Task<ApiResponseDto> UnlockConnectorAsync(int connectorId);

    }

}
