namespace PubSub.OcppServer.Models.Ocpp.v201;

public class TransactionEventResponse : IOcppResponse
{
    public decimal? TotalCost { get; set; }
    public int? ChargingPriority { get; set; }
    public IdTokenInfo? IdTokenInfo { get; set; }
    public MessageContent? UpdatedPersonalMessage { get; set; } 
}