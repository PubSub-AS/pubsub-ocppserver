using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Models.Ocpp.v16
{
    public partial class DataTransferResponse : IOcppResponse
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("data")]
        public string Data { get; set; }

        [JsonPropertyName("status")]
        public DataTransferStatus Status { get; set; }
    }
    

}
