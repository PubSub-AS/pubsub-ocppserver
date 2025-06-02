namespace PubSub.OcppServer.Models.Ocpp.v201;

public class GetVariablesRequest : IOcppRequest
{
    public GetVariableData[] GetVariableData { get; set; }
}