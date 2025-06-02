namespace PubSub.OcppServer.Models.Ocpp.v201;

public class GetDisplayMessagesResponse : IOcppResponse
{
    public GetDisplayMessagesStatusEnum Status { get; set; }
    public StatusInfo? StatusInfo { get; set; }
}