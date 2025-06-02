namespace PubSub.OcppServer.Models.Ocpp.v201;

public class ChargingProfileCriterion
{
    public ChargingProfilePurposeEnum? ChargingProfilePurpose { get; set; }
    public int? StackLevel { get; set; }
    public int[]? ChargingProfileId { get; set; }
    public ChargingLimitSourceEnum? ChargingLimitSource { get; set; }
}