using PubSub.OcppServer.Models.Ocpp.v16;

namespace PubSub.OcppServer.OcppMessageIncomingHandlers.v16
{
    public class FirmwareStatusNotificationIncomingHandler :
        IOcppMessageIncomingHandler<FirmwareStatusNotificationRequest, FirmwareStatusNotificationResponse>
    {
        private readonly ILogger<FirmwareStatusNotificationIncomingHandler> _logger;

        public FirmwareStatusNotificationIncomingHandler(ILogger<FirmwareStatusNotificationIncomingHandler> logger)
        {
            _logger = logger;
        }
        public FirmwareStatusNotificationResponse Handle(FirmwareStatusNotificationRequest request)
        {
            FirmwareStatusNotificationResponse response = new FirmwareStatusNotificationResponse();
            // implement support for notification receipt
            _logger.LogInformation("Received FirmwareStatusNotificationRequest. Doin' nothing about it.");

            // TODO: Do something about it

            return response;
        }
    }
}
