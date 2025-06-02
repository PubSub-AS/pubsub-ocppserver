namespace PubSub.OcppServer.Models.Ocpp.v201;

public class GetBaseReportRequest : IOcppRequest
{
    public int RequestId { get; set; }
    public ReportBaseEnum ReportBase { get; set; }
}