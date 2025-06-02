using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Models.Ocpp.v201
{
    public partial class GetDiagnosticsResponse : IOcppResponse
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("fileName")]
        [JsonConverter(typeof(MinMaxLengthCheckConverter))]
        public string FileName { get; set; }
    }
}