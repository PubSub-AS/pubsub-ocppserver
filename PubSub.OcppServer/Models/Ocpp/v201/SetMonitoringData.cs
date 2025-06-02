namespace PubSub.OcppServer.Models.Ocpp.v201;

public class SetMonitoringData
{
    public int? Id { get; set; }
    public bool? Transaction { get; set; }
    public decimal Value { get; set; }
    public MonitorEnum Type { get; set; }
}