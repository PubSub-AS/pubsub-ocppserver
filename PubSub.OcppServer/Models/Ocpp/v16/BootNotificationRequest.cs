using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Models.Ocpp.v16
{
    public partial class BootNotificationRequest : IOcppRequest
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("chargeBoxSerialNumber")]
        public string? ChargeBoxSerialNumber { get; set; }

        [JsonPropertyName("chargePointModel")]
        public string? ChargePointModel { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("chargePointSerialNumber")]
        public string? ChargePointSerialNumber { get; set; }

        [JsonPropertyName("chargePointVendor")]
        public string? ChargePointVendor { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("firmwareVersion")]
        public string? FirmwareVersion { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("iccid")]
        public string? Iccid { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("imsi")]
        public string? Imsi { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("meterSerialNumber")]
        public string? MeterSerialNumber { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("meterType")]
        public string? MeterType { get; set; }
    }
}
