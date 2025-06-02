namespace PubSub.OcppServer.Models.Ocpp.v201;

public class ReportData
{
    public Component Component { get; set; }
    public Variable Variable { get; set; }
    public VariableAttribute[] VariableAttribute { get; set; }
    public VariableCharacteristics VariableCharacteristics { get; set; }
}