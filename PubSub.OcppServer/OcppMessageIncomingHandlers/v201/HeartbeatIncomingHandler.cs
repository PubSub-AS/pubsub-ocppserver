using PubSub.OcppServer.Models.Ocpp.v201;
using PubSub.OcppServer.Services;

namespace PubSub.OcppServer.OcppMessageIncomingHandlers.v201
{
    public class HeartbeatIncomingHandler : IOcppMessageIncomingHandler<HeartbeatRequest, HeartbeatResponse>
    {
        private readonly ILogger<HeartbeatIncomingHandler> _logger;
        private readonly OcppHandlerContext _context;

        public HeartbeatIncomingHandler(ILogger<HeartbeatIncomingHandler> logger, OcppHandlerContext context)
        {
            _logger = logger;
            _context = context;
        }

        public HeartbeatResponse Handle(HeartbeatRequest request)
        {
            _logger.LogInformation($"Received Heartbeat for ChargingPoint: {_context.ChargingPointId}");
            return new HeartbeatResponse { CurrentTime = DateTime.UtcNow };
        }
    }

}
