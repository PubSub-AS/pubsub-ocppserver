namespace PubSub.OcppServer.Models.Ocpp.v201;

public class ACChargingParameters
{
    public int EnergyAmount { get; set; }
    public int EvMinCurrent { get; set;}
    public int EvMaxCurrent { get; set;}
    public int EvMaxVoltage { get; set; }
}