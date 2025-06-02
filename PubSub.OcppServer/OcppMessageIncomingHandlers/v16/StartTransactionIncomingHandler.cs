using PubSub.OcppServer.Models.Internal;
using PubSub.OcppServer.Models.Ocpp.v16;
using PubSub.OcppServer.Services.Interfaces;
using PubSub.OcppServer.Services;

namespace PubSub.OcppServer.OcppMessageIncomingHandlers.v16
{
    public class StartTransactionIncomingHandler : IOcppMessageIncomingHandler<StartTransactionRequest, StartTransactionResponse>
    {
        private readonly IOcppServer _ocppServer;
        private readonly ILogger<StartTransactionIncomingHandler> _logger;
        private readonly OcppHandlerContext _context;

        public StartTransactionIncomingHandler(IOcppServer ocppServer, ILogger<StartTransactionIncomingHandler> logger, OcppHandlerContext context)
        {
            _ocppServer = ocppServer;
            _logger = logger;
            _context = context;
        }

        public StartTransactionResponse Handle(StartTransactionRequest request)
        {
            _logger.LogInformation($"Starting transaction on Connector {request.ConnectorId} for ChargingPoint: {_context.ChargingPointId}");

            var idTagInfo = new IdTagInfo();

            if (!_ocppServer.IsIdTagValid(request.IdTag))
            {
                _logger.LogWarning($"Invalid IdTag {request.IdTag} for ChargingPoint {_context.ChargingPointId}");
                idTagInfo.Status = AuthorizationStatus.Invalid;
                return new StartTransactionResponse(idTagInfo, 0);
            }

            var connector = _context.Connectors.FirstOrDefault(c => c.ConnectorId == request.ConnectorId);

            if (connector == null)
            {
                connector = new ConnectorInternal
                {
                    ChargingPointId = _context.ChargingPointId,
                    ConnectorId = request.ConnectorId,
                    State = "Charging"
                };
                _context.Connectors.Add(connector);
            }

            var transaction = new TransactionInternal
            {
                ChargingPointId = _context.ChargingPointId,
                Connector = connector,
                IdTagId = request.IdTag,
                OcppVersion = OcppVersionEnum.v16,
                MeterValues = new List<MeterValueInternal>(),
                State = "Charging"
            };

            var transIds = _ocppServer.StartTransactionAsync(transaction).Result;
            transaction.ChargingTransactionIDOcppv16 = transIds.Item1;
            transaction.ChargingTransactionId = transIds.Item2;

            _ocppServer.StoreMeterValues(new List<MeterValueInternal>
        {
            new MeterValueInternal(transaction.ChargingTransactionId, request.Timestamp, (int)request.MeterStart)
        });

            connector.Transactions.Add(transaction);

            idTagInfo.Status = AuthorizationStatus.Accepted;
            return new StartTransactionResponse(idTagInfo, (long)transaction.ChargingTransactionIDOcppv16);
        }
    }

}
