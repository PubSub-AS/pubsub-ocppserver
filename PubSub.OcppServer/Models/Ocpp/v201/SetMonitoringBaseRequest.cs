namespace PubSub.OcppServer.Models.Ocpp.v201;

public class SetMonitoringBaseRequest : IOcppRequest
{
    public MonitoringBaseEnum MonitoringBase { get; set; }
}