using System.Text.Json.Serialization;
using PubSub.OcppServer.Models.EventArguments;

namespace PubSub.OcppServer.Models.Ocpp.v16
{
    public partial class BootNotificationResponse : IOcppResponse
    {
        [JsonPropertyName("currentTime")]
        public DateTimeOffset CurrentTime { get; set; }

        [JsonPropertyName("interval")]
        public long Interval { get; set; }

        [JsonPropertyName("status")]
        public RegistrationStatus Status { get; set; }
    }
    





   
   
        
}
