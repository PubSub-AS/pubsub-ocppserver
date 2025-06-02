namespace PubSub.OcppServer.Models.Ocpp.v201;

public class SetVariableMonitoringRequest : IOcppRequest
{
    public SetMonitoringData SetMonitoringData { get; set; }

}