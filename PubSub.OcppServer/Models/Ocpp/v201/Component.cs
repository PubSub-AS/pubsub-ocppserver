namespace PubSub.OcppServer.Models.Ocpp.v201;

public class Component
{
    public string Name { get; set; }
    public string? Instance { get; set; }
    public EVSE? Evse { get; set; }
}