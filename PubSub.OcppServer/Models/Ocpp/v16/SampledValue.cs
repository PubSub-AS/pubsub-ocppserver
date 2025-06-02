using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Models.Ocpp.v16
{
    public partial class SampledValue
    {
        public SampledValue() {}

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("context")]
        public string? Context { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("format")]
        public string? Format { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("location")]
        public string? Location { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("measurand")]
        public string? Measurand { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("phase")]
        public string? Phase { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("unit")]
        public string? Unit { get; set; }

      
        [JsonPropertyName("value")]
        public string? Value { get; set; }
    }
}
