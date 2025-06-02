using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Models.Ocpp.v16
{
    public partial class ResetRequest : IOcppRequest
    {
        [JsonPropertyName("type")] 
        public string Type { get; set; }
    }
}
