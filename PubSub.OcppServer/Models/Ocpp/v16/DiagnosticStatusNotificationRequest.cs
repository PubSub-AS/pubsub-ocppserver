using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Models.Ocpp.v16
{
    public partial class DiagnosticStatusNotificationRequest : IOcppRequest
    {
        [JsonPropertyName("status")]
        public FirmwareStatus Status { get; set; }
    }

}
