namespace PubSub.OcppServer.Models.Ocpp.v201;

public class GetDisplayMessagesRequest : IOcppRequest
{
    public int? Id { get; set; }
    public int RequestId { get; set; }
    public MessagePriorityEnum? Priority { get; set; }
    public MessageStateEnum? State { get; set; }
}