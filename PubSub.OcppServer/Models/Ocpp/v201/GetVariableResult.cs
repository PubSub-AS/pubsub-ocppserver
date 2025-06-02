namespace PubSub.OcppServer.Models.Ocpp.v201;

public class GetVariableResult
{
    public GetVariableStatusEnum AttributeStatus { get; set; }
    public AttributeEnum? AttributeType { get; set; }
    public string? AttributeValue { get; set; }
    public Component Component { get; set; }
    public Variable Variable { get; set; }
    public StatusInfo? AttributeStatusInfo { get; set; }
}