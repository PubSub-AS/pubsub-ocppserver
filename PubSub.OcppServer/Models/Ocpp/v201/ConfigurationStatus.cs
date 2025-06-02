using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Models.Ocpp.v201
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ConfigurationStatus
    {
        Accepted,
        Rejected,
        RebootRequired,
        NotSupported
    }
}
