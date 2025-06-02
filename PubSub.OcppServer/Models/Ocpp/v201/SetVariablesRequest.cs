namespace PubSub.OcppServer.Models.Ocpp.v201;

public class SetVariablesRequest : IOcppRequest
{
    public SetVariableData[] SetVariableData { get; set; }

}