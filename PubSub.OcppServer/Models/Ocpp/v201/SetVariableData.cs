namespace PubSub.OcppServer.Models.Ocpp.v201;

public class SetVariableData
{
    public AttributeEnum AttributeType { get; set; }
    public string AttributeValue { get; set; }
    public Component Component { get; set; }
    public Variable Variable { get; set; }
}