using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Models.Ocpp.v16
{
    public partial class StartTransactionRequest : IOcppRequest
    {
        public StartTransactionRequest(int connectorId, string idTag, long meterStart, long? reservationId, DateTimeOffset timestamp)
        {
            ConnectorId = connectorId;
            IdTag = idTag;
            MeterStart = meterStart;
            ReservationId = reservationId;
            Timestamp = timestamp;
        }

        [JsonPropertyName("connectorId")]
        public int ConnectorId { get; set; }

        [JsonPropertyName("idTag")]
        [JsonConverter(typeof(MinMaxLengthCheckConverter))]
        public string IdTag { get; set; }

        [JsonPropertyName("meterStart")]
        public long MeterStart { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("reservationId")]
        public long? ReservationId { get; set; }

        [JsonPropertyName("timestamp")]
        public DateTimeOffset Timestamp { get; set; }
    }
}