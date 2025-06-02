namespace PubSub.OcppServer.Models.Ocpp.v201;

public class GetTransactionStatusResponse
{
    public bool? OngoingIndicator { get; set; }
    public bool MessagesInQueue { get; set; }
}