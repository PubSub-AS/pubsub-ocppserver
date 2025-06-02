using PubSub.OcppServer.Data.Interfaces;
using PubSub.OcppServer.Models.Dtos;
using PubSub.OcppServer.Models.Internal;
using PubSub.OcppServer.Models.Ocpp.v201;
using PubSub.OcppServer.OcppMessageIncomingHandlers.v201;
using PubSub.OcppServer.Services.Interfaces;

namespace PubSub.OcppServer.Services
{
    public class OcppHandler201 : OcppHandler
    {
      
        public OcppHandler201 (IOcppMessageSerializer messageSerializer,
            ILogger<OcppHandler201> logger,
            IOcppServer ocppServer,
            //ISendMessageBus ocppClientSendMessageBus,
            IOcppClientManager webSocketManager,
            IUnitOfWork unitOfWork,
            OcppHandlerContext context,
            IOcppRequestManager ocppRequestManager,
            IChargingProfileService chargingProfileService,
            AuthorizeIncomingHandler authorizeIncomingHandler,
            BootNotificationIncomingHandler bootNotificationIncomingHandler,
            GetCertificateStatusIncomingHandler getCertificateStatusIncomingHandler,
            HeartbeatIncomingHandler heartbeatIncomingHandler,
            StatusNotificationIncomingHandler statusNotificationIncomingHandler,
            SecurityEventNotificationIncomingHandler securityEventNotificationIncomingHandler,
            IOcppMessageSerializer ocppMessageSerializer,
            ISendMessageBus sendMessageBus)
            : base(messageSerializer,
            logger,
            ocppServer,
            webSocketManager,
            unitOfWork,
            context,
            ocppRequestManager,
            chargingProfileService,
            ocppMessageSerializer, 
            sendMessageBus
            )
        {
            _ocppVersion = OcppVersionEnum.v201;
            _messageHandlers = new Dictionary<string, object>
            {
                { "Authorize", authorizeIncomingHandler },
                { "BootNotification", bootNotificationIncomingHandler },
                { "GetCertificateStatus", getCertificateStatusIncomingHandler },
                { "Heartbeat", heartbeatIncomingHandler },
                { "SecurityEventNotification", securityEventNotificationIncomingHandler },
                { "StatusNotification", statusNotificationIncomingHandler }
                
                /*                               
                { "DataTransfer", dataTransferHandler },
                { "DiagnosticStatusNotification", diagnosticStatusNotificationHandler },
                { "FirmwareStatusNotification", firmwareStatusNotificationHandler},               
                { "MeterValues", meterValuesHandler},
                { "StartTransaction", startTransactionHandler },              
                { "StopTransaction", stopTransactionHandler }
                */
            };
        }

        public override Task<ApiResponseDto> ClearCacheAsync()
        {
            throw new NotImplementedException();
        }

        public override async Task<ApiResponseDto> GetConfigurationAsync(List<string> configKeys)
        {
            var payload = new GetConfigurationRequest()
            {
                Key = configKeys.ToArray()
            };

            var errorOrResponse = await ParseAndSendOcppRequest<GetConfigurationRequest, GetConfigurationResponse>("GetConfiguration", payload);
            var ocppResponse = (GetConfigurationResponse)errorOrResponse.OcppResponse;

            var apiResponse = ApiResponseDto.CreateApiResponseObject(errorOrResponse);

            apiResponse.Data = new Dictionary<string, object>
            {
                { "ConfigurationKey", ocppResponse.ConfigurationKey },
                { "UnknownKey", ocppResponse.UnknownKey }
            };
            return apiResponse;
        }

        public override Task<ApiResponseDto> CancelReservationAsync(int reservationId)
        {
            throw new NotImplementedException();
        }

        public override async Task<ApiResponseDto> ChangeConfigurationAsync(string configKey, string configValue)
        {
            throw new NotImplementedException();
        }
        public override async Task<ApiResponseDto> ResetChargingPointAsync(bool isHard)
        {
            var resetType = isHard ? ResetEnum.Immediate : ResetEnum.OnIdle;
            var payload = new ResetRequest()
            {
                Type = resetType
            };
            var errorOrResponse = await ParseAndSendOcppRequest<ResetRequest, ResetResponse>("Reset", payload);
            return ApiResponseDto.CreateApiResponseObject(errorOrResponse);
        }

        public override Task<ApiResponseDto> ReserveNowAsync(int connectorId, DateTimeOffset expiryDate, string idTag)
        {
            throw new NotImplementedException();
        }

        public override async Task<ApiResponseDto> UnlockConnectorAsync(int connectorId)
        {
            throw  new NotImplementedException();
        }
    
        public override async Task<ApiResponseDto> RemoteStartTransactionAsync(int connectorId, string idTag)
        {
            throw new NotImplementedException();
        }
        public override async Task<ApiResponseDto> RemoteOptimizedStartTransactionAsync(int connectorId, string idTag, OptimizedChargingArgs optimizedChargingArgs)
        {
            throw new NotImplementedException();
        }
        public override async Task<ApiResponseDto> RemoteStopTransactionAsync(int transactionId)
        {
            throw new NotImplementedException();
        }
     

    }
}
