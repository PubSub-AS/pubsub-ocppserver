namespace PubSub.OcppServer.Models.Ocpp.v201;

public class RequestStartTransactionResponse : IOcppResponse
{
    public RequestStartStopStatusEnum Status { get; set; }
    public string? TransactionId { get; set; }
    public StatusInfo? StatusInfo { get; set; }
}