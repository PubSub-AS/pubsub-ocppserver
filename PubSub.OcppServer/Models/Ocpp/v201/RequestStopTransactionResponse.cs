namespace PubSub.OcppServer.Models.Ocpp.v201;

public class RequestStopTransactionResponse : IOcppResponse
{
    public RequestStartStopStatusEnum Status { get; set; }
    public StatusInfo? StatusInfo { get; set; }
}