using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Models.Ocpp.v16
{
    public partial class SendLocalListResponse : IOcppResponse
    {
        [JsonPropertyName("status")] public UpdateStatus Status { get; set; }
    }
}
