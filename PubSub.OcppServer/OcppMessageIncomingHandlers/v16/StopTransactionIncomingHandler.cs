using PubSub.OcppServer.Models.Internal;
using PubSub.OcppServer.Models.Ocpp.v16;
using PubSub.OcppServer.Services.Interfaces;
using PubSub.OcppServer.Services;

namespace PubSub.OcppServer.OcppMessageIncomingHandlers.v16
{
    public class StopTransactionIncomingHandler : IOcppMessageIncomingHandler<StopTransactionRequest, StopTransactionResponse>
    {
        private readonly IOcppServer _ocppServer;
        private readonly ILogger<StopTransactionIncomingHandler> _logger;
        private readonly OcppHandlerContext _context;

        public StopTransactionIncomingHandler(IOcppServer ocppServer, ILogger<StopTransactionIncomingHandler> logger, OcppHandlerContext context)
        {
            _ocppServer = ocppServer;
            _logger = logger;
            _context = context;
        }

        public StopTransactionResponse Handle(StopTransactionRequest request)
        {

            if (request.TransactionId < 0)
            {
                _logger.LogInformation("Client trying to stop an unknown transaction. " + request.Reason.ToString());
                return new StopTransactionResponse(new IdTagInfo { Status = AuthorizationStatus.Expired });
            }
            
            _logger.LogInformation($"Stopping transaction {request.TransactionId} for ChargingPoint: {_context.ChargingPointId}");

            var transaction = _ocppServer.GetChargingTransactionBy16Id(request.TransactionId);

            if (!_ocppServer.IsTransactionActive(transaction.ChargingTransactionId))
            {
                _logger.LogWarning($"Transaction {transaction.ChargingTransactionId} is not active.");
                return new StopTransactionResponse(new IdTagInfo { Status = AuthorizationStatus.Invalid });
            }
            
            var meterValues = new List<MeterValueInternal>
            {
                new (transaction.ChargingTransactionId, request.Timestamp, request.MeterStop)
            };
            
            // All meter values from the transaction are resent here. We'll ignore them for now.
            /*
            if (request.TransactionData != null)
            {
                foreach (var transactionData in request.TransactionData)
                {
                    foreach (var sampledValue in transactionData.SampledValue.Where(s => s.Phase == null))
                    {
                        meterValues.Add(new MeterValueInternal(transaction.ChargingTransactionId, sampledValue, transactionData.Timestamp));
                    }
                }
            }
            */

            if (meterValues.Any()) _ocppServer.StoreMeterValues(meterValues);
            _ocppServer.StopTransactionAsync(transaction.ChargingTransactionId);

            var connector = _context.Connectors.FirstOrDefault(c => c.Transactions.Any(t => t.ChargingTransactionId == transaction.ChargingTransactionId));

            connector?.Transactions.RemoveAll(t => t.ChargingTransactionId == transaction.ChargingTransactionId);

            return new StopTransactionResponse(new IdTagInfo { Status = AuthorizationStatus.Accepted });
        }
    }

}
