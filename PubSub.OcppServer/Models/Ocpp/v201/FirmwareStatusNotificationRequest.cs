using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Models.Ocpp.v201
{
    public partial class FirmwareStatusNotificationRequest : IOcppRequest
    {
        public FirmwareStatusEnum Status { get; set; }
        public int? RequestId { get; set; }
    }
}