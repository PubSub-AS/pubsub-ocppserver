namespace PubSub.OcppServer.Models.Ocpp.v201;

public class TriggerMessageRequest : IOcppRequest
{
    public MessageTriggerEnum RequestedMessage { get; set; }
    public EVSE? Evse { get; set; }
}