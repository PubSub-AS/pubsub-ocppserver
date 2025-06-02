using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Models.Ocpp.v201
{
    public partial class GetConfigurationRequest : IOcppRequest
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("key")]
        //[JsonConverter(typeof(DecodeArrayConverter))]
        public string[] Key { get; set; }
    }
}