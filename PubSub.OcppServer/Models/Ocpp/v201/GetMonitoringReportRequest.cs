using System.Security.Cryptography;

namespace PubSub.OcppServer.Models.Ocpp.v201;

public class GetMonitoringReportRequest : IOcppRequest
{
    public int RequestId { get; set; }
    public MonitoringCriterionEnum[] MonitoringCriteria { get; set; }
    public ComponentVariable[] ComponentVariable { get; set; }
}