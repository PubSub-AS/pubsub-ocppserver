using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Models.Ocpp.v201
{
    public class SetChargingProfileRequest : IOcppRequest
    {
        public int EvseId { get; set; }
        
        public ChargingProfile ChargingProfile { get; set; }
    }

}