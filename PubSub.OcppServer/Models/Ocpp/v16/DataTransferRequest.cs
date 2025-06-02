using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Models.Ocpp.v16
{
    public partial class DataTransferRequest : IOcppRequest
    {
        public DataTransferRequest(string data, string messageId, string vendorId)
        {
            Data = data;
            MessageId = messageId;
            VendorId = vendorId;
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("data")]
        public string Data { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("messageId")]
        public string MessageId { get; set; }

        [JsonPropertyName("vendorId")]
        public string VendorId { get; set; }
    }
}