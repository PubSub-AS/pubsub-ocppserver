namespace PubSub.OcppServer.Models.Ocpp.v201;

public class GetLogResponse : IOcppResponse
{
    public LogStatusEnum Status { get; set; }
    public string? Filename { get; set; }
    public StatusInfo? StatusInfo { get; set; }
}