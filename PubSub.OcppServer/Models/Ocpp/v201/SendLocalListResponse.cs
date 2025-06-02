using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Models.Ocpp.v201
{
    public class SendLocalListResponse : IOcppResponse
    {
      public SendLocalListStatusEnum Status { get; set; }
      public StatusInfo? StatusInfo { get; set; }
    }

    public enum SendLocalListStatusEnum
    {
        Accepted, Failed, VersionMismatch
    }
}
