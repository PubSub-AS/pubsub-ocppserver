using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Models.Ocpp.v16
{
    public partial class ChangeAvailabilityRequest : IOcppRequest
    {
        [JsonPropertyName("connectorId")] public long ConnectorId { get; set; }

        [JsonPropertyName("type")] public TypeEnum Type { get; set; }
    }
}