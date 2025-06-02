namespace PubSub.OcppServer.Models.Ocpp.v201;

public class SetVariablesResponse : IOcppResponse
{
    public SetVariableResult SetVariableResult { get; set; }
}