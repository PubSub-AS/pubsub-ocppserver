namespace PubSub.OcppServer.Models.Ocpp.v201;

public class ClearChargingProfileType
{
    public int? EvseId { get; set; }
    public ChargingProfilePurposeEnum? ChargingProfilePurpose { get; set;}
}