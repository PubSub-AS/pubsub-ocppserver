using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Models.Ocpp.v201
{
    public partial class StopTransactionRequest : IOcppRequest
    {
        public StopTransactionRequest(string idTag, long meterStop, Reason? reason, DateTimeOffset timestamp, TransactionDatum[] transactionData, long transactionId)
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

        [JsonPropertyName("meterStop")] public long MeterStop { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("reason")]
        public Reason? Reason { get; set; }

        [JsonPropertyName("timestamp")] public DateTimeOffset Timestamp { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("transactionData")]
        public TransactionDatum[] TransactionData { get; set; }

        [JsonPropertyName("transactionId")] public long TransactionId { get; set; }
    }
}