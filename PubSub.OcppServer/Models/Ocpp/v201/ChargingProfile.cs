using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Models.Ocpp.v201
{
    public partial class ChargingProfile
    {
        public ChargingProfile(long chargingProfileId, ChargingProfileKind chargingProfileKind, ChargingProfilePurposeEnum chargingProfilePurpose, ChargingSchedule chargingSchedule, RecurrencyKind? recurrencyKind, long stackLevel, long? transactionId, DateTimeOffset? validFrom, DateTimeOffset? validTo)
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
        public long ChargingProfileId { get; set; }

        [JsonPropertyName("chargingProfileKind")]
        public ChargingProfileKind ChargingProfileKind { get; set; }

        [JsonPropertyName("chargingProfilePurpose")]
        public ChargingProfilePurposeEnum ChargingProfilePurpose { get; set; }

        [JsonPropertyName("chargingSchedule")]
        public ChargingSchedule ChargingSchedule { get; set; }

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
