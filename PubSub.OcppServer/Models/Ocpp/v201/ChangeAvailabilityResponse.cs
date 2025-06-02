using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Models.Ocpp.v201
{
    public partial class ChangeAvailabilityResponse : IOcppResponse
    {
        public ChangeAvailabilityStatusEnum Status { get; set; }
        public StatusInfo? StatusInfo { get; set; }
    }
}