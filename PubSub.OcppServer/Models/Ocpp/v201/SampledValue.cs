using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Models.Ocpp.v201
{
    public partial class SampledValue
    {
        public SampledValue() {}
        public SampledValue(Context? context, Format? format, Location? location, Measurand? measurand, Phase? phase, Unit? unit, string value)
        {
            Context = context;
            Format = format;
            Location = location;
            Measurand = measurand;
            Phase = phase;
            Unit = unit;
            Value = value;
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("context")]
        public Context? Context { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("format")]
        public Format? Format { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("location")]
        public Location? Location { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("measurand")]
        public Measurand? Measurand { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("phase")]
        public Phase? Phase { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("unit")]
        public Unit? Unit { get; set; }

      
        [JsonPropertyName("value")]
        public string? Value { get; set; }
    }
}
