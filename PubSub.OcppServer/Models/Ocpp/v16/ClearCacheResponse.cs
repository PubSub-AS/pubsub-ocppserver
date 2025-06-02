using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Models.Ocpp.v16
{
    public partial class ClearCacheResponse : IOcppResponse
    {
        [JsonPropertyName("status")] public ClearCacheStatus Status { get; set; }
    }
}