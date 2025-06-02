namespace PubSub.OcppServer.Models.Ocpp.v201;

public class NotifyChargingLimitRequest : IOcppRequest
{
    public int EvseId { get; set; }
    public ChargingLimit ChargingLimit { get; set; }
    public ChargingSchedule? ChargingSchedule { get; set; }
}