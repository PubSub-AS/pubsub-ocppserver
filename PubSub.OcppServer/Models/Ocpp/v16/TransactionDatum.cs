using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Models.Ocpp.v16
{
    public partial class TransactionDatum
    {
        public TransactionDatum(SampledValue[] sampledValue, DateTimeOffset timestamp)
        {
            SampledValue = sampledValue;
            Timestamp = timestamp;
        }

        [JsonPropertyName("sampledValue")]
        public SampledValue[] SampledValue { get; set; }

        [JsonPropertyName("timestamp")]
        public DateTimeOffset Timestamp { get; set; }
    }
}
