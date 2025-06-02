namespace PubSub.OcppServer.Models.Ocpp.v201;

public class SecurityEventNotificationRequest : IOcppRequest
{
    public string Type { get; set; }
    public DateTime Timestamp { get; set; }
    public string? TechInfo { get; set; }
}