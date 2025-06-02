namespace PubSub.OcppServer.Models.Ocpp.v201;

public class EventData
{
    public int EventId { get; set; }
    public DateTime Timestamp { get; set; }
    public EventTriggerEnum Trigger { get; set; }
    public int? Cause { get; set; }
    public string ActualValue { get; set; }
    public string? TechCode { get; set; }
    public string? TechInfo { get; set; }
    public bool? Cleared { get; set; }
    public string? TransactionId { get; set; }
    public int? VariableMonitoringId { get; set; }
    public EventNotificationEnum EventNotificationType { get; set; }
    public Variable Variable { get; set; }
}