using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Models.Ocpp.v16
{
    public partial class ResetResponse : IOcppResponse
    {
        [JsonPropertyName("status")] public ResetStatus Status { get; set; }
    }
}