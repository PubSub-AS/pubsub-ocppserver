namespace PubSub.OcppServer.Models.Ocpp.v201;

public class NotifyEvChargingNeedsRequest : IOcppRequest
{
    public int? MaxScheduleTuples { get; set; }
    public int EvseId { get; set; }
    public ChargingNeeds ChargingNeeds { get; set; }
}