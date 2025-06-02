namespace PubSub.OcppServer.Models.Ocpp.v201;

public class TransactionEventRequest : IOcppRequest
{
    public TransactionEventEnum EventType { get; set; }
    public DateTime Timestamp { get; set; }
    public TriggerReasonEnum TriggerReason { get; set; }
    public int SeqNo { get; set; }
    public bool? Offline { get; set; }
    public int? NumberOfPhasesUsed { get; set; }
    public int? CableMaxCurrent { get; set; }
    public int? ReservationId { get; set; }
    public Transaction TransactionInfo { get; set; }
    public IdTokenType? IdToken { get; set; }
    public EVSE? Evse { get; set; }
    public MeterValue[]? MeterValue { get; set; }
}