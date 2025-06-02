namespace PubSub.OcppServer.Models.Ocpp.v201;

public class MessageInfo
{
    public int Id { get; set; }
    public MessagePriorityEnum Priority { get; set; }
    public MessageStateEnum? State { get; set; }
    public DateTime? StartDateTime { get; set; }
    public DateTime? EndDateTime { get; set; }
    public string TransactionId { get; set; }
    public MessageContent Message { get; set; }
    public Component Display { get; set; }
}