using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Models.Ocpp.v201
{
    public partial class ChangeConfigurationResponse : IOcppResponse
    {
        [JsonPropertyName("status")] public ConfigurationStatus Status { get; set; }
    }
}
