using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Models.Ocpp.v16
{
    public partial class UnlockConnectorResponse : IOcppResponse
    {
        [JsonPropertyName("status")]
        public UnlockStatus Status { get; set; }
    }

}
