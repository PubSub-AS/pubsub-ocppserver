using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Models.Ocpp.v201
{
    public class ChargingSchedulePeriod
    {
        public decimal Limit { get; set; }
        public int? NumberPhases { get; set; }
        public int StartPeriod { get; set; }
        public int? PhaseToUse { get; set; }
    }
}
