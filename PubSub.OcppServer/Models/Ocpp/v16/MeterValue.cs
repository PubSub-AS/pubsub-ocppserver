using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Models.Ocpp.v16
{
    public class MeterValue
    {
        [JsonPropertyName("sampledValue")]
        public SampledValue[] SampledValue { get; set; }

        [JsonPropertyName("timestamp")]
        public DateTimeOffset Timestamp { get; set; }
    }
}
