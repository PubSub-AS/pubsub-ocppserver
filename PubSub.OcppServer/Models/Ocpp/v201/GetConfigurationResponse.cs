using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Models.Ocpp.v201
{
    public partial class GetConfigurationResponse : IOcppResponse
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
