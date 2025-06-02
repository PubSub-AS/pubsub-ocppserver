using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Models.Ocpp.v201
{
    public class TriggerMessageResponse : IOcppResponse
    {
        public TriggerMessageStatusEnum Status { get; set; }
        public StatusInfo? StatusInfo { get; set; }
    }

}