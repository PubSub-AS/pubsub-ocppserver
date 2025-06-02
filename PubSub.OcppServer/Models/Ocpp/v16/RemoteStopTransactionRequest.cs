using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Models.Ocpp.v16
{
    public class RemoteStopTransactionRequest : IOcppRequest
    {
        [JsonPropertyName("transactionId")]
        public int TransactionId { get; set; }
    }
}