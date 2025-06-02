namespace PubSub.OcppServer.Models.Ocpp.v201;

public class NotifyEVChargingScheduleRequest : IOcppRequest
{
    public DateTime TimeBase { get; set; }
    public int EvseId { get; set; }
    public ChargingSchedule ChargingSchedule { get; set; }
}