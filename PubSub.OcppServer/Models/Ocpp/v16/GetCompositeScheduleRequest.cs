using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Models.Ocpp.v16
{
    public partial class GetCompositeScheduleRequest : IOcppRequest
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("chargingRateUnit")]
        public ChargingRateUnit? ChargingRateUnit { get; set; }

        [JsonPropertyName("connectorId")] public long ConnectorId { get; set; }

        [JsonPropertyName("duration")] public long Duration { get; set; }
    }

}