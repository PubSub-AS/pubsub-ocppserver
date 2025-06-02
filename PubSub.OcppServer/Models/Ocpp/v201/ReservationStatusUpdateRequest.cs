namespace PubSub.OcppServer.Models.Ocpp.v201;

public class ReservationStatusUpdateRequest : IOcppRequest
{
    public int ReservationId { get; set; }
    public ReservationUpdateStatusEnum ReservationUpdateStatus { get; set; }
}