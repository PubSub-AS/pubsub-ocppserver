namespace PubSub.OcppServer.Models.Ocpp.v201;

public class Transaction
{
    public string TransactionId { get; set; }
    public ChargingStateEnum? ChargingState { get; set; } 
    public int? TimeSpentCharging { get; set; } 
    public ReasonEnum? StoppedReason { get; set; }
    public int? RemoteStartId { get; set; }
}