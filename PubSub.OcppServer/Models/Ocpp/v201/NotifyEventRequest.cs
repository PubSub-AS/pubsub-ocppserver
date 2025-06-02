namespace PubSub.OcppServer.Models.Ocpp.v201;

public class NotifyEventRequest : IOcppRequest
{
    public DateTime GeneratedAt { get; set; }
    public bool? Tbc { get; set; }
    public int SeqNo { get; set; }
    public EventData[] EventData { get; set; }
}