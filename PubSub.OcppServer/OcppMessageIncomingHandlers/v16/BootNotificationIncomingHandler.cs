using PubSub.OcppServer.Models.Ocpp.v16;
using PubSub.OcppServer.Services;
using PubSub.OcppServer.Services.Interfaces;

namespace PubSub.OcppServer.OcppMessageIncomingHandlers.v16
{
    public class BootNotificationIncomingHandler : IOcppMessageIncomingHandler<BootNotificationRequest, BootNotificationResponse>
    {
        private readonly IOcppServer _ocppServer;
        private readonly ILogger<BootNotificationIncomingHandler> _logger;
        private readonly OcppHandlerContext _context;  // Inject the context

        public BootNotificationIncomingHandler(
            IOcppServer ocppServer,
            ILogger<BootNotificationIncomingHandler> logger,
            OcppHandlerContext context)
        {
            _ocppServer = ocppServer;
            _logger = logger;
            _context = context;
        }

        public BootNotificationResponse Handle(BootNotificationRequest request)
        {
            _logger.LogInformation($"Handling Boot Notification for ChargingPoint: {_context.ChargingPointId}");

            _ocppServer.StoreChargingPointInfo(
                _context.ChargingPointId,
                request.ChargePointSerialNumber,
                request.FirmwareVersion,
                request.ChargePointModel
            );

            return new BootNotificationResponse
            {
                CurrentTime = DateTime.UtcNow,
                Status = RegistrationStatus.Accepted
            };
        }
    }


}
