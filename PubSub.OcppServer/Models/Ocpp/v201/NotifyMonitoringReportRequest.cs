namespace PubSub.OcppServer.Models.Ocpp.v201;

public class NotifyMonitoringReportRequest : IOcppRequest
{
    public int RequestId { get; set; }
    public bool? Tbc { get; set; }
    public int SeqNo { get; set; }
    public DateTime GeneratedAt { get; set; }
    public MonitoringData[]? Monitor { get; set; } 
}