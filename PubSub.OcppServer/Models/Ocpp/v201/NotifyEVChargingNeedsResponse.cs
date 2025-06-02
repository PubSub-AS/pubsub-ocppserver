namespace PubSub.OcppServer.Models.Ocpp.v201;

public class NotifyEVChargingNeedsResponse
{
    public NotifyEVChargingNeedsStatusEnum Status { get; set; }
    public StatusInfo? StatusInfo { get; set; }
}