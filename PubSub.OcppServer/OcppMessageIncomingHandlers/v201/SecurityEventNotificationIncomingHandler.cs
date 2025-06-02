using PubSub.OcppServer.Models.Ocpp.v201;
using PubSub.OcppServer.Services;
using PubSub.OcppServer.Services.Interfaces;

namespace PubSub.OcppServer.OcppMessageIncomingHandlers.v201
{
    public class SecurityEventNotificationIncomingHandler : IOcppMessageIncomingHandler<SecurityEventNotificationRequest, SecurityEventNotificationResponse>
    {
        private readonly IOcppServer _ocppServer;
        private readonly ILogger<SecurityEventNotificationIncomingHandler> _logger;
        private readonly OcppHandlerContext _context;  // Inject the context

        public SecurityEventNotificationIncomingHandler(
            IOcppServer ocppServer,
            ILogger<SecurityEventNotificationIncomingHandler> logger,
            OcppHandlerContext context)
        {
            _ocppServer = ocppServer;
            _logger = logger;
            _context = context;
        }

        public SecurityEventNotificationResponse Handle(SecurityEventNotificationRequest request)
        {
            _logger.LogInformation($"Received Security Event Notification for ChargingPoint: {_context.ChargingPointId}");
            _logger.LogInformation("Type" + request.Type);
            if (request.TechInfo != null) _logger.LogInformation("TechInfo: " + request.TechInfo);

            return new SecurityEventNotificationResponse();

        }
    }


}
