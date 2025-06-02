using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Models.Ocpp.v16
{
    public partial class TriggerMessageResponse : IOcppResponse
    {
        [JsonPropertyName("status")] public TriggerMessageStatus Status { get; set; }
    }
}