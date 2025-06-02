using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Models.Ocpp.v201
{
    public partial class ClearChargingProfileResponse : IOcppResponse
    {
        public ClearChargingProfileStatusEnum Status { get; set; }
        public StatusInfo? StatusInfo { get; set; }
    }

}
