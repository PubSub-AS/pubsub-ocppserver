namespace PubSub.OcppServer.Models.Ocpp.v201;

public class SetMonitoringLevelRequest : IOcppRequest
{
    public int Severity { get; set; }

}