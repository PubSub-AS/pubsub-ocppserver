using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Models.Ocpp.v201
{
    public class UnlockConnectorResponse : IOcppResponse
    {
        public UnlockStatusEnum Status { get; set; }
        public StatusInfo? StatusInfo { get; set; }
    }
}
