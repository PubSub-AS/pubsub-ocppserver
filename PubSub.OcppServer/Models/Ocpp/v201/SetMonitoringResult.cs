namespace PubSub.OcppServer.Models.Ocpp.v201;

public class SetMonitoringResult
{
    public int? Id { get; set; }
    public SetMonitoringStatusEnum Status { get; set; }
    public MonitorEnum Type { get; set; }
    public int Severity { get; set; }
    public Component Component { get; set; }
    public Variable Variable { get; set; }
    public StatusInfo? StatusInfo { get; set; }
}