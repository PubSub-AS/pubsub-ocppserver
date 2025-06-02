using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Models.Ocpp.v201
{
    public partial class BootNotificationRequest : IOcppRequest
    {
        public ChargingStation ChargingStation { get; set; }
        public BootReasonEnum Reason { get; set; }

    }
}
