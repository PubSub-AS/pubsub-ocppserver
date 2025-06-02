using PubSub.OcppServer.Models.Internal;
using PubSub.OcppServer.Models.Ocpp.v16;
using PubSub.OcppServer.Services.Interfaces;
using PubSub.OcppServer.Services;

namespace PubSub.OcppServer.OcppMessageIncomingHandlers.v16
{
    public class MeterValuesIncomingHandler : IOcppMessageIncomingHandler<MeterValuesRequest, MeterValuesResponse>
    {
        private readonly IOcppServer _ocppServer;
        private readonly ILogger<MeterValuesIncomingHandler> _logger;
        private readonly OcppHandlerContext _context;

        public MeterValuesIncomingHandler(IOcppServer ocppServer, ILogger<MeterValuesIncomingHandler> logger, OcppHandlerContext context)
        {
            _ocppServer = ocppServer;
            _logger = logger;
            _context = context;
        }

        public MeterValuesResponse Handle(MeterValuesRequest request)
        {
            if (request.TransactionId == null)
            {
                _logger.LogCritical("Metervalues received without transaction ID. Make it possible to create a transaction on the fly ");
                return new MeterValuesResponse();
            }
            _logger.LogInformation($"Handling MeterValues for Transaction {request.TransactionId}");

            var transaction = _ocppServer.GetChargingTransactionBy16Id((int)request.TransactionId);

            if (transaction == null)
            {
                _logger.LogWarning($"Transaction {request.TransactionId} not found.");
                return new MeterValuesResponse();
            }

            var meterValues = new List<MeterValueInternal>();

            foreach (var meterValue in request.MeterValue)
            {
                foreach (var sampledValue in meterValue.SampledValue.Where(s => s.Phase == null))
                {
                    meterValues.Add(new MeterValueInternal(transaction.ChargingTransactionId, sampledValue, meterValue.Timestamp));
                }
            }

            _ocppServer.StoreMeterValues(meterValues);

            return new MeterValuesResponse();
        }
    }

}
