using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Models.Ocpp.v201
{
    public partial class GetDiagnosticsRequest : IOcppRequest
    {
        [JsonPropertyName("location")]
        public Uri Location { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("retries")]
        public long? Retries { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("retryInterval")]
        public long? RetryInterval { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("startTime")]
        public DateTimeOffset? StartTime { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("stopTime")]
        public DateTimeOffset? StopTime { get; set; }
    }
}