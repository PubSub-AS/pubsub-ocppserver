using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Models.Ocpp.v201
{
    public partial class ClearChargingProfileRequest : IOcppRequest
    {
        public int? ClearChargingProfileId { get; set; }
        public ClearChargingProfileType? ChargingProfileCriteria { get; set; }
}
}