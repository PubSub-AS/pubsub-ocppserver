using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Models.Ocpp.v201
{
    public partial class CancelReservationResponse : IOcppResponse
    {
        public CancelReservationStatus Status { get; set; }
        public StatusInfo? StatusInfo { get; set; }
    }
}
