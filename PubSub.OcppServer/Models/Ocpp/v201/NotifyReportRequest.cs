namespace PubSub.OcppServer.Models.Ocpp.v201;

public class NotifyReportRequest : IOcppRequest
{
    public int RequestId { get; set; }
    public DateTime GeneratedAt { get; set; }
    public bool? Tbc { get; set; }
    // SeqNo starting at 0
    public int SeqNo { get; set; }
    public ReportData? ReportData { get; set; }
}