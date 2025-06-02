namespace PubSub.OcppServer.Models.Ocpp.v201;

public class ChargingLimit
{
    public ChargingLimitSourceEnum ChargingLimitSource { get; set; }
    public bool IsGridCritical { get; set; }
}