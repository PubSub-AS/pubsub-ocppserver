using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Models.Ocpp.v16
{
    public partial class UnlockConnectorRequest : IOcppRequest
    {
        [JsonPropertyName("connectorId")]
        public int ConnectorId { get; set; }
    }
}