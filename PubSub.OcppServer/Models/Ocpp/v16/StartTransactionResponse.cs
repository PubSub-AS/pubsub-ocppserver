using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Models.Ocpp.v16
{
    public partial class StartTransactionResponse : IOcppResponse
    {
        public StartTransactionResponse(IdTagInfo idTagInfo, long transactionId)
        {
            IdTagInfo = idTagInfo;
            TransactionId = transactionId;
        }

        [JsonPropertyName("idTagInfo")] public IdTagInfo IdTagInfo { get; set; }

        [JsonPropertyName("transactionId")] public long TransactionId { get; set; }
    }
}
