namespace PubSub.OcppServer.Models.Ocpp.v201;

public class NotifyEVChargingScheduleResponse : IOcppResponse
{
    public GenericStatusEnum Status { get; set; }
    public StatusInfo? StatusInfo { get; set; }
}