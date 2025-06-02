namespace PubSub.OcppServer.Models.Ocpp.v201;

public class NotifyDisplayMessagesRequest : IOcppRequest
{
    public int RequestId { get; set; }
    public bool? Tbc { get; set; }
    public MessageInfo MessageInfo { get; set; }
}