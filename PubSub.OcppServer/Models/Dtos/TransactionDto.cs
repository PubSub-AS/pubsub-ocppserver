using PubSub.OcppServer.Models.Internal;

namespace PubSub.OcppServer.Models.Dtos
{
    public class TransactionDto
    {
        public string ChargingTransactionId { get; set; }
        public string ChargingPointId { get; set; }
        public ConnectorDto Connector { get; set; }
        public string State { get; set; }
        public string IdTagId { get; set; }
        public List<MeterValueDto> MeterValues { get; set; }
        public DateTimeOffset LastUpdated { get; set; }
        public double TotalKWh { get; set; }
        public int TotalSeconds { get; set; }
        public double TotalPriceEuros { get; set; }
        public int Soc { get; set; }
        public int EffectWatts { get; set; }
    }
}
