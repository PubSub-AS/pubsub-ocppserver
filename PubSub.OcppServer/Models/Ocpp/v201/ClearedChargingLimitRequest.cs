namespace PubSub.OcppServer.Models.Ocpp.v201;

public class ClearedChargingLimitRequest : IOcppRequest
{
    public ChargingLimitSourceEnum ChargingLimitSource { get; set; }
    public int? EvseId { get; set; }
}