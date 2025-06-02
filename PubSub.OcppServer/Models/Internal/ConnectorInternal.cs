using PubSub.OcppServer.Models.EF;

namespace PubSub.OcppServer.Models.Internal
{
    public class ConnectorInternal
    {
        public int ConnectorId { get; set; }
        public string ChargingPointId { get; set; }
        public string State { get; set; }
        public List<TransactionInternal> Transactions { get; set; } = new List<TransactionInternal>();
    }
}
