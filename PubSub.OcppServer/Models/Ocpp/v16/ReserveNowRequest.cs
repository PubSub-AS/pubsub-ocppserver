using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Models.Ocpp.v16
{
    public partial class ReserveNowRequest : IOcppRequest
    {
        public ReserveNowRequest(int connectorId, DateTimeOffset expiryDate, string idTag, string parentIdTag, int reservationId)
        {
            ConnectorId = connectorId;
            ExpiryDate = expiryDate;
            IdTag = idTag;
            ParentIdTag = parentIdTag;
            ReservationId = reservationId;
        }

        [JsonPropertyName("connectorId")]
        public int ConnectorId { get; set; }

        [JsonPropertyName("expiryDate")]
        public DateTimeOffset ExpiryDate { get; set; }

        [JsonPropertyName("idTag")]
        [JsonConverter(typeof(MinMaxLengthCheckConverter))]
        public string IdTag { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("parentIdTag")]
        [JsonConverter(typeof(MinMaxLengthCheckConverter))]
        public string ParentIdTag { get; set; }

        [JsonPropertyName("reservationId")]
        public int ReservationId { get; set; }
    }
}