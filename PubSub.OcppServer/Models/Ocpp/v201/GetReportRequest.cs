namespace PubSub.OcppServer.Models.Ocpp.v201;

public class GetReportRequest : IOcppRequest 
{
    public int RequestId { get; set; }
    public ComponentCriterionEnum ComponentCriteria { get; set; }
    public ComponentVariable ComponentVariable { get; set; }
}