namespace PubSub.OcppServer.Models.Internal
{
    public class TransactionInternal
    {
        public string ChargingTransactionId { get; set; }
        public int? ChargingTransactionIDOcppv16 { get; set; }
        public OcppVersionEnum OcppVersion { get; set; }
        public string ChargingPointId { get; set; }
        public ConnectorInternal Connector { get; set; }
        public string State { get; set; }
        public string IdTagId { get; set; }
        public List<MeterValueInternal> MeterValues { get; set; }
        public DateTimeOffset LastUpdated { get; set; }
        public TransactionInternal()
        {
            MeterValues = new List<MeterValueInternal>();
        }

        public bool IsAuthorized()
            => string.IsNullOrEmpty(IdTagId);
    }
}
