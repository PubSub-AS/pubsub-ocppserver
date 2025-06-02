using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Models.Ocpp.v201
{
    public partial class ClearCacheResponse : IOcppResponse
    {
        public ClearCacheStatusEnum Status { get; set; }
        public StatusInfo? StatusInfo { get; set; }
    }
}