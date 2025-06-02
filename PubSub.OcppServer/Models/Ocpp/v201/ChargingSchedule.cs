using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Models.Ocpp.v201
{
    public class ChargingSchedule
    {

        public DateTimeOffset? StartSchedule { get; set; }
        public int? Duration { get; set; }
        public ChargingRateUnitEnum ChargingRateUnit { get; set; }
        public decimal? MinChargingRate { get; set; } 
        public ChargingSchedulePeriod[] ChargingSchedulePeriod { get; set; }
        
    }
}
