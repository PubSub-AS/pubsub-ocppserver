using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Models.Ocpp.v201
{
    public class CancelReservationRequest : IOcppRequest
    {
        public int ReservationId { get; set; }
    }
}
