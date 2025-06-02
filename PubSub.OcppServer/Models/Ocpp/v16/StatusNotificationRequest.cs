using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Models.Ocpp.v16
{
    public partial class StatusNotificationRequest : IOcppRequest
    {
        public StatusNotificationRequest(int connectorId, ErrorCode errorCode, string info, ChargePointStatus status, DateTimeOffset? timestamp, string vendorErrorCode, string vendorId)
        {
            ConnectorId = connectorId;
            ErrorCode = errorCode;
            Info = info;
            Status = status;
            Timestamp = timestamp;
            VendorErrorCode = vendorErrorCode;
            VendorId = vendorId;
        }

        [JsonPropertyName("connectorId")]
        public int ConnectorId { get; set; }

        [JsonPropertyName("errorCode")]
        public ErrorCode ErrorCode { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("info")]
        public string Info { get; set; }

        [JsonPropertyName("status")]
        public ChargePointStatus Status { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("timestamp")]
        public DateTimeOffset? Timestamp { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("vendorErrorCode")]
        public string VendorErrorCode { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("vendorId")]
        public string VendorId { get; set; }

    }
}
