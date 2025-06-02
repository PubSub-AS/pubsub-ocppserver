using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Models.Ocpp.v16
{
    public class ConfigurationKey
    {
        public ConfigurationKey(string key, bool @readonly, string value)
        {
            Key = key;
            Readonly = @readonly;
            Value = value;
        }

        [JsonPropertyName("key")]
        public string Key { get; set; }

        [JsonPropertyName("readonly")]
        public bool Readonly { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("value")]
        public string Value { get; set; }
    }
}
