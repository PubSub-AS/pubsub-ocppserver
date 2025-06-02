using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Models.Ocpp.v16
{
    public partial class RemoteStartTransactionRequest : IOcppRequest
    {
        public RemoteStartTransactionRequest(ChargingProfile? chargingProfile, int? connectorId, string idTag)
        {
            ChargingProfile = chargingProfile;
            ConnectorId = connectorId;
            IdTag = idTag;
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("chargingProfile")]
        public ChargingProfile? ChargingProfile { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("connectorId")]
        public int? ConnectorId { get; set; }

        [JsonPropertyName("idTag")]
        [JsonConverter(typeof(MinMaxLengthCheckConverter))]
        public string IdTag { get; set; }
    }
}
