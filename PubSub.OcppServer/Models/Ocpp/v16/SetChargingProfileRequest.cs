using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Models.Ocpp.v16
{
    public partial class SetChargingProfileRequest : IOcppRequest
    {
        public SetChargingProfileRequest(long connectorId, v201.CsChargingProfiles csChargingProfiles)
        {
            ConnectorId = connectorId;
            CsChargingProfiles = csChargingProfiles;
        }

        [JsonPropertyName("connectorId")] public long ConnectorId { get; set; }

        [JsonPropertyName("csChargingProfiles")]
        public v201.CsChargingProfiles CsChargingProfiles { get; set; }
    }
}