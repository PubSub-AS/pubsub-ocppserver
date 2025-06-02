namespace PubSub.OcppServer.Models.Ocpp.v201;

public class RequestStartTransactionRequest : IOcppRequest
{
    public int? EvseId { get; set; }
    public int RemoteStartId { get; set; }
    public IdTokenType IdToken { get; set; }
    public ChargingProfile? ChargingProfile { get; set; }
    public IdTokenType? GroupIdToken { get; set; }
}