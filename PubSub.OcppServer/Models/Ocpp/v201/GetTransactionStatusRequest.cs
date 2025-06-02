namespace PubSub.OcppServer.Models.Ocpp.v201;

public class GetTransactionStatusRequest : IOcppRequest
{
    public string? TransactionId { get; set; }
}