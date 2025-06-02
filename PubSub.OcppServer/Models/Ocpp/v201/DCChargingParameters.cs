namespace PubSub.OcppServer.Models.Ocpp.v201;

public class DCChargingParameters
{
    public int EvMaxCurrent { get; set;}
    public int EvMaxVoltage { get; set;}
    public int? EnergyAmount { get; set; }
    public int? EvMaxPower { get; set; }
    public int? StateOfCharge { get; set;}
    public int? EvEnergyCapacity { get; set; }
    public int? FullSoC { get; set;}
    public int? BulkSoC { get; set;}
}