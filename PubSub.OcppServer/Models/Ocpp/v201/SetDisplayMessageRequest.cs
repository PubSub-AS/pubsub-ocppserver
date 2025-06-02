namespace PubSub.OcppServer.Models.Ocpp.v201;

public class SetDisplayMessageRequest : IOcppRequest
{
    public MessageInfo Message { get; set; }
}