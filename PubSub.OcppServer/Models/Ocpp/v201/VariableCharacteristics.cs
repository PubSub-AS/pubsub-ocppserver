namespace PubSub.OcppServer.Models.Ocpp.v201;

public class VariableCharacteristics
{
    public string? Unit { get; set; }
    public DataEnum DataType { get; set; }
    public decimal? MinLimit { get; set; }
    public decimal? MaxLimit { get; set; }
    public string? ValuesList { get; set; }
    public bool SupportsMonitoring { get; set; }
}