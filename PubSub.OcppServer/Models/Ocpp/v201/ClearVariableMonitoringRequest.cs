namespace PubSub.OcppServer.Models.Ocpp.v201;

public class ClearVariableMonitoringRequest : IOcppRequest
{
    public List<int> Id { get; set; }

}