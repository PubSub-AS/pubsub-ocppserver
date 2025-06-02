using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Models.Ocpp.v16
{
    public class ClearChargingProfileResponse : IOcppResponse
    {
        [JsonPropertyName("status")]
        public ClearChargingProfileStatus Status { get; set; }
    }

}
