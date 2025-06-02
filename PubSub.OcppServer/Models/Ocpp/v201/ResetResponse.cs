using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Models.Ocpp.v201
{
    public partial class ResetResponse : IOcppResponse
    {
        public ResetStatusEnum Status { get; set; }
        public StatusInfo? StatusInfo { get; set; }
    }
}