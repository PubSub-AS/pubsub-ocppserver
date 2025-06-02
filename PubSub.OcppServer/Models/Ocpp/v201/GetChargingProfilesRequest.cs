namespace PubSub.OcppServer.Models.Ocpp.v201;

public class GetChargingProfilesRequest : IOcppRequest
{
    public int RequestId { get; set; }
    public int? EvseId { get; set; }
    public ChargingProfileCriterion ChargingProfile { get; set; }
}