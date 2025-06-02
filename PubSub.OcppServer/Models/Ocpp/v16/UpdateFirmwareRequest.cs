using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Models.Ocpp.v16
{
    public class UpdateFirmwareRequest : IOcppRequest
    {
        public UpdateFirmwareRequest(Uri location, long? retries, DateTimeOffset retrieveDate, long? retryInterval)
        {
            Location = location;
            Retries = retries;
            RetrieveDate = retrieveDate;
            RetryInterval = retryInterval;
        }

        [JsonPropertyName("location")]
        public Uri Location { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("retries")]
        public long? Retries { get; set; }

        [JsonPropertyName("retrieveDate")]
        public DateTimeOffset RetrieveDate { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("retryInterval")]
        public long? RetryInterval { get; set; }
    }
}