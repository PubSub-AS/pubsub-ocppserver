namespace PubSub.OcppServer.Models.Ocpp.v201;

public class GetLogRequest : IOcppRequest
{
    public LogEnum LogType { get; set; }
    public int RequestId { get; set; }
    public int? Retries { get; set; }
    public int? RetryInterval { get; set; }
    public LogParameters Log { get; set; }
}