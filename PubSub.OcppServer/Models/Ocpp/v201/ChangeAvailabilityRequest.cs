
namespace PubSub.OcppServer.Models.Ocpp.v201
{
    public partial class ChangeAvailabilityRequest : IOcppRequest
    {
       
        public OperationalStatusEnum OperationalStatus { get; set; }
        public EVSE Evse { get; set; }

    }
}