using System.Globalization;
using PubSub.OcppServer.Models.Ocpp.v16;

namespace PubSub.OcppServer.Models.Internal
{
    public record MeterValueInternal : IComparable<MeterValueInternal>
    {
        public string MeterValueID { get; set; }
        public string ChargingTransactionID { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public string? Context { get; set; }
        public bool FormatIsRawNotSignedData { get; set; }
        public double? ValueRaw { get; set; }
        public string? ValueSignedData { get; set; }
        public string? Unit { get; set; }
        public string? Location { get; set; }
        public string? Measurand { get; set; }
        public string? Phase { get; set; }

        public MeterValueInternal()
        {

        }
        public MeterValueInternal(string transactionId, 
            SampledValue sampledValue, 
            DateTimeOffset timestamp) : this(transactionId, timestamp)
        {

            Context = sampledValue.Context ?? "Sample.Periodic";
            Location = sampledValue.Location ?? "Outlet";
            Phase = sampledValue.Phase;
            Measurand = sampledValue.Measurand ?? "Energy.Active.Import.Register";
            if (sampledValue.Format is "Raw" or null)
            {
                // should use TryParse
                var parsedValueRaw = double.Parse(sampledValue.Value, CultureInfo.InvariantCulture);
                if (sampledValue.Unit == "Wh" || (sampledValue.Unit == null && Measurand.StartsWith("Energy")))
                {
                    Unit = "kWh";
                    ValueRaw = parsedValueRaw / 1000;
                }
                else
                {
                    Unit = sampledValue.Unit;
                    ValueRaw = parsedValueRaw;
                }
                FormatIsRawNotSignedData = true;
            }
            else
            {
                ValueSignedData = sampledValue.Value;
                FormatIsRawNotSignedData = false;
            }
        }

        public MeterValueInternal(string transactionId, DateTimeOffset timestamp)
        {
            ChargingTransactionID = transactionId;
            MeterValueID = Guid.NewGuid().ToString();
            Timestamp = timestamp.ToUniversalTime();
        }
        public MeterValueInternal(
            string transactionId, 
            DateTimeOffset timestamp,
            int meterSampleWh) : this (transactionId, timestamp)
        {
            Unit = "kWh";
            ValueRaw = (double) meterSampleWh/1000;
            FormatIsRawNotSignedData = true;
        }
        public int CompareTo(MeterValueInternal? other)
        {
            return other == null ? 0 : Timestamp.CompareTo(other.Timestamp);
        }
    }
}
