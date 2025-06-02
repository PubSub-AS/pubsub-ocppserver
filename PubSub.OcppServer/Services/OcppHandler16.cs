
using PubSub.OcppServer.Data.Interfaces;
using PubSub.OcppServer.Models.Dtos;
using PubSub.OcppServer.Models.Internal;
using PubSub.OcppServer.Models.Ocpp.v16;
using ResetTypeEnum = PubSub.OcppServer.Models.Internal.ResetTypeEnum;
using PubSub.OcppServer.Services.Interfaces;
using PubSub.OcppServer.Models.EF;
using PubSub.OcppServer.Models.Ocpp;
using PubSub.OcppServer.OcppMessageIncomingHandlers;
using PubSub.OcppServer.OcppMessageIncomingHandlers.v16;

namespace PubSub.OcppServer.Services
{
    public class OcppHandler16 : OcppHandler
    {
      
        public OcppHandler16 (IOcppMessageSerializer messageSerializer,
            ILogger<OcppHandler16> logger,
            IOcppServer ocppServer,
            IOcppClientManager webSocketManager,
            IUnitOfWork unitOfWork,
            OcppHandlerContext context,
            IOcppRequestManager ocppRequestManager,
            IChargingProfileService chargingProfileService,
            IOcppMessageSerializer ocppMessageSerializer,
            ISendMessageBus sendMessageBus,
            AuthorizeIncomingHandler authorizeIncomingHandler,
            BootNotificationIncomingHandler bootNotificationIncomingHandler,
            DataTransferIncomingHandler dataTransferIncomingHandler,
            DiagnosticStatusNotificationIncomingHandler diagnosticStatusNotificationIncomingHandler,
            FirmwareStatusNotificationIncomingHandler firmwareStatusNotificationIncomingHandler,
            HeartbeatIncomingHandler heartbeatIncomingHandler,
            MeterValuesIncomingHandler meterValuesIncomingHandler,
            SecurityEventNotificationIncomingHandler securityEventNotificationIncomingHandler,
            StartTransactionIncomingHandler startTransactionIncomingHandler,
            StatusNotificationIncomingHandler statusNotificationIncomingHandler,
            StopTransactionIncomingHandler stopTransactionIncomingHandler)
            : base(messageSerializer,
            logger,
            ocppServer,
            webSocketManager,
            unitOfWork,
            context,
            ocppRequestManager,
            chargingProfileService,
            ocppMessageSerializer,
            sendMessageBus)
        {
            _ocppVersion = OcppVersionEnum.v16;
            _messageHandlers = new Dictionary<string, object>
            {
                { "Authorize", authorizeIncomingHandler },
                { "BootNotification", bootNotificationIncomingHandler },
                { "DataTransfer", dataTransferIncomingHandler },
                { "DiagnosticStatusNotification", diagnosticStatusNotificationIncomingHandler },
                { "FirmwareStatusNotification", firmwareStatusNotificationIncomingHandler},
                { "Heartbeat", heartbeatIncomingHandler },
                { "MeterValues", meterValuesIncomingHandler},
                { "SecurityEventNotification", securityEventNotificationIncomingHandler },
                { "StartTransaction", startTransactionIncomingHandler },
                { "StatusNotification", statusNotificationIncomingHandler },
                { "StopTransaction", stopTransactionIncomingHandler }

            };
        }


        public override async Task<ApiResponseDto> CancelReservationAsync(int reservationId)
        {
            var payload = new CancelReservationRequest()
            {
                ReservationId = reservationId
            };
            var errorOrResponse = await ParseAndSendOcppRequest<CancelReservationRequest, CancelReservationResponse>("CancelReservation", payload);
            return ApiResponseDto.CreateApiResponseObject(errorOrResponse);
        }


        public override async Task<ApiResponseDto> ChangeConfigurationAsync(string configKey, string configValue)
        {
            var payload = new ChangeConfigurationRequest()
            {
                Key = configKey,
                Value = configValue
            };
            var errorOrResponse = await ParseAndSendOcppRequest<ChangeConfigurationRequest, ChangeConfigurationResponse>("ChangeConfiguration", payload);
            return ApiResponseDto.CreateApiResponseObject(errorOrResponse);
   
        }
        public override async Task<ApiResponseDto> ClearCacheAsync()
        {
            var payload = new ClearCacheRequest();
            var errorOrResponse = await ParseAndSendOcppRequest<ClearCacheRequest, ClearCacheResponse>("ClearCache", payload);
            return ApiResponseDto.CreateApiResponseObject(errorOrResponse);
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

        public override async Task<ApiResponseDto> ReserveNowAsync(int connectorId, DateTimeOffset expiryDate, string idTag)
        {
            var nextReservationId =
                _unitOfWork
                    .Reservations
                    .GetFirstAvailableReservationId();
            var payload = new ReserveNowRequest(connectorId, expiryDate, idTag, "", nextReservationId);
            var errorOrResponse = await ParseAndSendOcppRequest<ReserveNowRequest, ReserveNowResponse>("ReserveNow", payload);
            var apiResponse = ApiResponseDto.CreateApiResponseObject(errorOrResponse);
            var ocppResponse = (ReserveNowResponse) errorOrResponse.OcppResponse;
            if (ocppResponse.Status != ReservationStatus.Accepted)
                return apiResponse;
            var chargingPointId = _context.ChargingPointId;

            var connector = _unitOfWork
                .Connectors.Find(c => c.ChargingPointId == chargingPointId && c.ConnectorName == connectorId)
                .FirstOrDefault();
            if (connector == null)
            {
                _logger.LogInformation("Didn't find Connector for " + chargingPointId + " " + connectorId);
                _unitOfWork.Complete();
                return apiResponse;
            }

            var reservation = new Reservation()
            {
                ChargingPointId = connector.ChargingPointId,
                ConnectorId = connector.ConnectorId,
                ExpiryDate = expiryDate,
                IdTagId = idTag
            };
            _unitOfWork
                .Reservations
                .Add(reservation);
            // Fix the possible race condittion
            _unitOfWork.Complete();
            return apiResponse;
        }
        public override async Task<ApiResponseDto> RemoteStartTransactionAsync(int connectorId, string idTag)
        {
            var payload = new RemoteStartTransactionRequest(null, connectorId, idTag);
            var errorOrResponse = await ParseAndSendOcppRequest<RemoteStartTransactionRequest, RemoteStartTransactionResponse>("RemoteStartTransaction", payload);
            return ApiResponseDto.CreateApiResponseObject(errorOrResponse);
        }
        public override async Task<ApiResponseDto> RemoteOptimizedStartTransactionAsync(
            int connectorId, 
            string idTag, 
            OptimizedChargingArgs optimizedChargingArgs)
        {
            var profile = await _chargingProfileService.Create(
                optimizedChargingArgs,
                _context.ChargingPointId,
                connectorId);
            var payload = new RemoteStartTransactionRequest(profile, connectorId, idTag);
            var errorOrResponse = await ParseAndSendOcppRequest<RemoteStartTransactionRequest, RemoteStartTransactionResponse>("RemoteStartTransaction", payload);
            return ApiResponseDto.CreateApiResponseObject(errorOrResponse);
        }

        public override async Task<ApiResponseDto> RemoteStopTransactionAsync(int connectorId)
        {
            var v16Id = _unitOfWork
                .ChargingTransactions
                .Find(c => c.ChargingPointID == _context.ChargingPointId && c.ConnectorName == connectorId)
                .Max(v => v.v16Id);

            if (v16Id == 0)
                return new ApiResponseDto()
                {
                    Status = ApiResponseStatusEnum.TransactionUnknown,
                    StatusMessage = "Transaction not found in database"
                };
            var payload = new RemoteStopTransactionRequest()
            {
                TransactionId = v16Id
            };
            var errorOrResponse = await ParseAndSendOcppRequest<RemoteStopTransactionRequest, RemoteStopTransactionResponse>(
                "RemoteStopTransaction", payload);
            return ApiResponseDto.CreateApiResponseObject(errorOrResponse);
        }
        public override async Task<ApiResponseDto> ResetChargingPointAsync(bool isHard)
        {
            var resetType = isHard ? ResetTypeEnum.Hard : ResetTypeEnum.Soft;
            var payload = new ResetRequest()
            {
                Type = resetType == ResetTypeEnum.Hard ? "Hard" : "Soft"
            };
            var errorOrResponse = await ParseAndSendOcppRequest<ResetRequest, ResetResponse>("Reset", payload);
            return ApiResponseDto.CreateApiResponseObject(errorOrResponse);
        }

        public override async Task<ApiResponseDto> UnlockConnectorAsync(int connectorId)
        {
            var payload = new UnlockConnectorRequest()
            {
                ConnectorId = connectorId
            };
            var errorOrResponse = await ParseAndSendOcppRequest<UnlockConnectorRequest, UnlockConnectorResponse>("UnlockConnector",
                payload);
            return ApiResponseDto.CreateApiResponseObject(errorOrResponse);
        }
        

      
    }

}
