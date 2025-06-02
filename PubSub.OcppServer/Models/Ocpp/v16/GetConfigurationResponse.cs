using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Models.Ocpp.v16
{
    public class GetConfigurationResponse : IOcppResponse
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("configurationKey")]
        public ConfigurationKey[] ConfigurationKey { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("unknownKey")]
        //[JsonConverter(typeof(DecodeArrayConverter))]
        public string[] UnknownKey { get; set; }
    }
}
