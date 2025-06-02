using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Models.Ocpp.v16
{
    public partial class CsChargingProfiles
    {
        [JsonPropertyName("chargingProfileId")]
        public long ChargingProfileId { get; set; }

        [JsonPropertyName("chargingProfileKind")]
        public ChargingProfileKind ChargingProfileKind { get; set; }

        [JsonPropertyName("chargingProfilePurpose")]
        public ChargingProfilePurpose ChargingProfilePurpose { get; set; }

        [JsonPropertyName("chargingSchedule")]
        public v201.ChargingSchedule ChargingSchedule { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("recurrencyKind")]
        public RecurrencyKind? RecurrencyKind { get; set; }

        [JsonPropertyName("stackLevel")]
        public long StackLevel { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("transactionId")]
        public long? TransactionId { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("validFrom")]
        public DateTimeOffset? ValidFrom { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("validTo")]
        public DateTimeOffset? ValidTo { get; set; }
    }
}
