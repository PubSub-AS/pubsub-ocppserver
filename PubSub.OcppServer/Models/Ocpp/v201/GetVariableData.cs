namespace PubSub.OcppServer.Models.Ocpp.v201;

public class GetVariableData
{
    public AttributeEnum? AttributeStatus { get; set; }
    public Component Component { get; set; }
    public Variable Variable { get; set; }
}