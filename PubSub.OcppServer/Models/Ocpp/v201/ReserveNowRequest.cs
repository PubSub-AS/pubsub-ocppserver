namespace PubSub.OcppServer.Models.Ocpp.v201;

public class ReserveNowRequest : IOcppRequest
{
    public int Id { get; set; }
    public DateTime ExpiryDateTime { get; set; }
    public ConnectorEnum? ConnectorType { get; set; }
    public int? EvseId { get; set; }
    public IdTokenType IdToken { get; set; }

}