using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Models.Ocpp.v201
{
    public partial class BootNotificationResponse : IOcppResponse
    {
        public DateTimeOffset CurrentTime { get; set; }
        public int Interval { get; set; }
        public RegistrationStatusEnum Status { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public StatusInfo StatusInfo { get; set; }
    }
}
