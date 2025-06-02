using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Models.Ocpp.v16
{
    public partial class ChargingSchedulePeriod
    {
        [JsonPropertyName("limit")]
        public double Limit { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("numberPhases")]
        public long? NumberPhases { get; set; }

        [JsonPropertyName("startPeriod")]
        public long StartPeriod { get; set; }
    }
}
