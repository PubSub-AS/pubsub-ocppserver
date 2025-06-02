namespace PubSub.OcppServer.Models.Ocpp.v201;

public class GetVariablesResponse : IOcppResponse
{
    public GetVariableResult GetVariableResult { get; set; }
}