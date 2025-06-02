using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Models.Ocpp.v16
{
    public partial class ChargingSchedule
    {
        public ChargingSchedule(ChargingRateUnit chargingRateUnit, ChargingSchedulePeriod[] chargingSchedulePeriod, int? duration, double? minChargingRate, DateTimeOffset? startSchedule)
        {
            ChargingRateUnit = chargingRateUnit;
            ChargingSchedulePeriod = chargingSchedulePeriod;
            Duration = duration;
            MinChargingRate = minChargingRate;
            StartSchedule = startSchedule;
        }

        [JsonPropertyName("chargingRateUnit")]
        public ChargingRateUnit ChargingRateUnit { get; set; }

        [JsonPropertyName("chargingSchedulePeriod")]
        public ChargingSchedulePeriod[] ChargingSchedulePeriod { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("duration")]
        public int? Duration { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("minChargingRate")]
        public double? MinChargingRate { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("startSchedule")]
        public DateTimeOffset? StartSchedule { get; set; }
    }
}
