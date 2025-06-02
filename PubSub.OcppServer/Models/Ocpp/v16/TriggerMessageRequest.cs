using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Models.Ocpp.v16
{
    public partial class TriggerMessageRequest : IOcppRequest
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("connectorId")]
        public long? ConnectorId { get; set; }

        [JsonPropertyName("requestedMessage")] public RequestedMessage RequestedMessage { get; set; }
    }
}