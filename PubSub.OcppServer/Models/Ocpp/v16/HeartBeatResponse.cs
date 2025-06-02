using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Models.Ocpp.v16
{
    public partial class HeartbeatResponse : IOcppResponse
    {
        [JsonPropertyName("currentTime")]
        public DateTimeOffset CurrentTime { get; set; }

        public HeartbeatResponse()
        {
            CurrentTime = DateTimeOffset.Now;
        }
    }
}