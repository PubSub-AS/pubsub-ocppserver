using PubSub.OcppServer.Models.Internal;

namespace PubSub.OcppServer.Models.EF
{
    public class MeterValue : IComparable<MeterValue>
    {
        public string MeterValueID { get; set; }
        public string ChargingTransactionID { get; set; }
        public ChargingTransaction ChargingTransaction { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public double? ValueRaw { get; set; }
        public string? ValueSignedData { get; set; } 
        public string? Unit { get; set; }
        public string? Context { get; set; }
        public string? Location { get; set; }
        public string? Measurand { get; set; }
        public string? Phase { get; set; }
        public double? PriceEuros { get; set; }

        public int CompareTo(MeterValue other)
        {
            return Timestamp.CompareTo(other.Timestamp);
        }
    }
}
