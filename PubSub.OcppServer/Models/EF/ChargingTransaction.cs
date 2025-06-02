using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PubSub.OcppServer.Models.EF
{
    public class ChargingTransaction
    {
        [Key]
        public string ChargingTransactionID { get; set; }
        public int v16Id { get; set; }
        
        public int ConnectorName { get; set; }
        [ForeignKey(nameof(ConnectorName))]
        public Connector Connector { get; set; }
        public ChargingPoint ChargingPoint { get; set; }
        public string ChargingPointID { get; set; }
        public string IdTagID { get; set; }
        public IdTag IdTag { get; set; }
        public string State { get; set; }
        public DateTimeOffset LastUpdated { get; set; }
        public ICollection<MeterValue> MeterValues { get; set; }
        public double? TotalKWh { get; set; }
        public int? TotalSeconds { get; set; }
        public double? TotalPriceEuros { get; set; }
        public int? Soc { get; set; }
        public int? EffectWatts { get; set; }
        public Evse? Evse { get; set; }
        public ICollection<ChargingSchedulePeriod> ChargingSchedulePeriods { get; set; }
        public ChargingTransaction()
        {
            MeterValues = new List<MeterValue>();
        }

        public void CalculateTotalKWh()
        {
            var minKWh = MeterValues
                .Where(m => m.Unit == "kWh")
                .Min(m => m.ValueRaw);
            var maxKWh = MeterValues
                .Where(m => m.Unit == "kWh")
                .Max(m => m.ValueRaw);
            if (minKWh != null && maxKWh != null) TotalKWh = maxKWh - minKWh;

        }
        public void CalculateTotalSeconds()
        {
            var minTimestamp = MeterValues
                .Min(m => m.Timestamp);
            var maxTimestamp = MeterValues
                .Max(m => m.Timestamp);
            TotalSeconds = (int)(maxTimestamp - minTimestamp).TotalSeconds;

        }

        public void CalculatePrice()
        {
            TotalPriceEuros = MeterValues.Sum(m => m.PriceEuros);
        }
    }
}
