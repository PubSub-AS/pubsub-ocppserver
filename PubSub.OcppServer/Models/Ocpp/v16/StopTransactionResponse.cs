using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Models.Ocpp.v16
{
    public partial class StopTransactionResponse : IOcppResponse
    {
        public StopTransactionResponse(IdTagInfo idTagInfo)
        {
            IdTagInfo = idTagInfo;
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("idTagInfo")]
        public IdTagInfo IdTagInfo { get; set; }
    }
}