namespace PubSub.OcppServer.Models.Ocpp.v201;

internal class ClearMonitoringResult
{
    public ClearMonitoringStatusEnum Status { get; set; }
    public int Id { get; set; }
    public StatusInfo? StatusInfo { get; set; }
}