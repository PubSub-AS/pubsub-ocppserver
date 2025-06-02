namespace PubSub.OcppServer.Models.Ocpp.v201;

public class MonitoringData
{
    public Component Component { get; set; }
    public Variable Variable { get; set; }
    public VariableMonitoring[] VariableMonitoring { get; set; }
}