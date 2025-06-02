namespace PubSub.OcppServer.Models.Ocpp.v201;

public class ClearDisplayMessageResponse
{
    public ClearMessageStatusEnum Status { get; set; }
    public StatusInfo? StatusInfo { get; set; } 
}