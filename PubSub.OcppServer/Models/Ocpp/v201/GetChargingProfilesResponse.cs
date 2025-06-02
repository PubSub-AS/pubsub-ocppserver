namespace PubSub.OcppServer.Models.Ocpp.v201;

public class GetChargingProfilesResponse : IOcppResponse
{
    public GetChargingProfileStatusEnum Status { get; set; }
    public StatusInfo? StatusInfo { get; set; }
}