using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Models.Ocpp.v201
{
    public partial class StopTransactionResponse : IOcppResponse
    {
        public StopTransactionResponse(IdTokenInfo idTagInfo)
        {
            IdTagInfo = idTagInfo;
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("idTagInfo")]
        public IdTokenInfo IdTagInfo { get; set; }
    }
}