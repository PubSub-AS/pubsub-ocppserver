namespace PubSub.OcppServer.Models.Ocpp.v201;

public class SetNetworkProfileResponse
{
    public SetNetworkProfileStatusEnum Status { get; set; }
    public StatusInfo? StatusInfo { get; set; }
}