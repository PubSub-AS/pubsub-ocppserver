namespace PubSub.OcppServer.Models.Ocpp.v201;

public class RequestStopTransactionRequest : IOcppRequest
{
    public string TransactionId { get; set; }
}