using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Models.Ocpp.v201
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum UpdateStatus
    {
        Accepted,
        Failed,
        NotSupported,
        VersionMismatch
    }
}
