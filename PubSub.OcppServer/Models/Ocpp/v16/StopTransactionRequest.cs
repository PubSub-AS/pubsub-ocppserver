using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Models.Ocpp.v16
{
    public partial class StopTransactionRequest : IOcppRequest
    {
        public StopTransactionRequest(string idTag, int meterStop, Reason? reason, DateTimeOffset timestamp, TransactionDatum[] transactionData, int transactionId)
        {
            IdTag = idTag;
            MeterStop = meterStop;
            Reason = reason;
            Timestamp = timestamp;
            TransactionData = transactionData;
            TransactionId = transactionId;
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("idTag")]
        [JsonConverter(typeof(MinMaxLengthCheckConverter))]
        public string IdTag { get; set; }

        [JsonPropertyName("meterStop")] 
        public int MeterStop { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("reason")]
        public Reason? Reason { get; set; }

        [JsonPropertyName("timestamp")] public DateTimeOffset Timestamp { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("transactionData")]
        public TransactionDatum[] TransactionData { get; set; }

        [JsonPropertyName("transactionId")] public int TransactionId { get; set; }
    }
}