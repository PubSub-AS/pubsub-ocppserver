using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Models.Ocpp.v16
{
    public class ChargingProfile
    {
        public ChargingProfile(int chargingProfileId, ChargingProfileKind chargingProfileKind, ChargingProfilePurpose chargingProfilePurpose, ChargingSchedule chargingSchedule, RecurrencyKind? recurrencyKind, int stackLevel, int? transactionId, DateTimeOffset? validFrom, DateTimeOffset? validTo)
        {
            ChargingProfileId = chargingProfileId;
            ChargingProfileKind = chargingProfileKind;
            ChargingProfilePurpose = chargingProfilePurpose;
            ChargingSchedule = chargingSchedule;
            RecurrencyKind = recurrencyKind;
            StackLevel = stackLevel;
            TransactionId = transactionId;
            ValidFrom = validFrom;
            ValidTo = validTo;
        }

        [JsonPropertyName("chargingProfileId")]
        public int ChargingProfileId { get; set; }

        [JsonPropertyName("chargingProfileKind")]
        public ChargingProfileKind ChargingProfileKind { get; set; }

        [JsonPropertyName("chargingProfilePurpose")]
        public ChargingProfilePurpose ChargingProfilePurpose { get; set; }

        [JsonPropertyName("chargingSchedule")]
        public ChargingSchedule ChargingSchedule { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("recurrencyKind")]
        public RecurrencyKind? RecurrencyKind { get; set; }

        [JsonPropertyName("stackLevel")]
        public int StackLevel { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("transactionId")]
        public int? TransactionId { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("validFrom")]
        public DateTimeOffset? ValidFrom { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("validTo")]
        public DateTimeOffset? ValidTo { get; set; }
    }
}
