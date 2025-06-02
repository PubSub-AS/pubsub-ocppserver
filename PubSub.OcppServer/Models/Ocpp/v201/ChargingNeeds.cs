namespace PubSub.OcppServer.Models.Ocpp.v201;

public class ChargingNeeds
{
    public EnergyTransferModeEnum RequestedEnergyTransfer { get; set; }
    public DateTime? DepartureTime { get; set; }
    public ACChargingParameters AcChargingParameters { get; set; }
    public DCChargingParameters DcChargingParameters { get; set; } 
}