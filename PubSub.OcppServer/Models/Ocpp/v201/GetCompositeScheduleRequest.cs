using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Models.Ocpp.v201
{
    public partial class GetCompositeScheduleRequest : IOcppRequest
    {
        public ChargingRateUnitEnum? ChargingRateUnit { get; set; }
        public int EvseId { get; set; }
        public int Duration { get; set; }
    }

}