namespace PubSub.OcppServer.Models.Ocpp.v201;

public class ReportChargingProfilesRequest : IOcppRequest
{
    public int RequestId { get; set; }
    public ChargingLimitSourceEnum ChargingLimitSource { get; set; }
    public bool? Tbc { get; set; }
    public int EvseId { get; set; }
    public ChargingProfile[] ChargingProfile { get; set; }
}