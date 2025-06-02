using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Models.Ocpp.v16
{
    public partial class ChangeConfigurationResponse : IOcppResponse
    {
        [JsonPropertyName("status")] public ConfigurationStatus Status { get; set; }
    }
}
