using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Models.Ocpp.v201
{
    public partial class StartTransactionResponse : IOcppResponse
    {
        public StartTransactionResponse(IdTokenInfo idTagInfo, long transactionId)
        {
            IdTagInfo = idTagInfo;
            TransactionId = transactionId;
        }

        [JsonPropertyName("idTagInfo")] public IdTokenInfo IdTagInfo { get; set; }

        [JsonPropertyName("transactionId")] public long TransactionId { get; set; }
    }
}
