namespace PubSub.OcppServer.Models.EventArguments;

public class MeterValueArgs : EventArgs
{
    public string MeterValueID { get; set; }
    public string ChargingPointId { get; set; }
    public string ChargingTransactionID { get; set; }
    public DateTimeOffset Timestamp { get; set; }
    public string Value { get; set; }
    public string? Unit { get; set; }
}