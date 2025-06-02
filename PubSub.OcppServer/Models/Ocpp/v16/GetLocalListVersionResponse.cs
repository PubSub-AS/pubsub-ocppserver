using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Models.Ocpp.v16
{
    public partial class GetLocalListVersionResponse : IOcppResponse
    {
        [JsonPropertyName("listVersion")]
        public long ListVersion { get; set; }
    }
}