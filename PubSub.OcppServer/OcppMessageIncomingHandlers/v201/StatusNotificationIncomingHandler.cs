using PubSub.OcppServer.Models.Internal;
using PubSub.OcppServer.Models.Ocpp.v201;
using PubSub.OcppServer.Services.Interfaces;
using PubSub.OcppServer.Services;

namespace PubSub.OcppServer.OcppMessageIncomingHandlers.v201
{
    public class StatusNotificationIncomingHandler : IOcppMessageIncomingHandler<StatusNotificationRequest, StatusNotificationResponse>
    {
        private readonly IOcppServer _ocppServer;
        private readonly ILogger<StatusNotificationIncomingHandler> _logger;
        private readonly OcppHandlerContext _context;

        public StatusNotificationIncomingHandler(IOcppServer ocppServer, ILogger<StatusNotificationIncomingHandler> logger, OcppHandlerContext context)
        {
            _ocppServer = ocppServer;
            _logger = logger;
            _context = context;
        }

        public StatusNotificationResponse Handle(StatusNotificationRequest request)
        {
            _logger.LogInformation($"Status Notification for ChargingPoint {_context.ChargingPointId}, Connector {request.ConnectorId}");

            var connector = _context.Connectors.FirstOrDefault(c => c.ConnectorId == request.ConnectorId);
            var state = Enum.GetName(typeof(ConnectorStatusEnum), request.ConnectorStatus) ?? "Unknown";
            if (connector == null)
            {
                connector = new ConnectorInternal
                {
                    ChargingPointId = _context.ChargingPointId,
                    ConnectorId = request.ConnectorId,
                    State = state
                };
                _context.Connectors.Add(connector);
            }
            else
            {
                connector.State = state;
            }
            if (request.ConnectorId != 0)
                _ocppServer.SetConnectorState(_context.ChargingPointId, request.ConnectorId, connector.State);

            return new StatusNotificationResponse();
        }
    }

}
