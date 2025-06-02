using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Models.Ocpp.v201
{
    public partial class DiagnosticStatusNotificationRequest : IOcppRequest
    {
        [JsonPropertyName("status")]
        public FirmwareStatusEnum Status { get; set; }
    }

}
