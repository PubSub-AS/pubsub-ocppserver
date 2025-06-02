namespace PubSub.OcppServer.Models.Ocpp.v201;

public class CompositeSchedule
{
    public int EvseId { get; set; }
    public int Duration { get; set; }
    public DateTime ScheduleStart { get; set; }
    public ChargingRateUnitEnum ChargingRateUnit { get; set; }
    public ChargingSchedulePeriod[] ChargingSchedulePeriod { get; set; }
}