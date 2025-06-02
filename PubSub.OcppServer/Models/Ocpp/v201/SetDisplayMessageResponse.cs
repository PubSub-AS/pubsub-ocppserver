namespace PubSub.OcppServer.Models.Ocpp.v201;

public class SetDisplayMessageResponse : IOcppResponse
{
    private DisplayMessageStatusEnum Status { get; set; }
    public StatusInfo? StatusInfo { get; set; }
}