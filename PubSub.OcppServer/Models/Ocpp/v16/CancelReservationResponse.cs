using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Models.Ocpp.v16
{
    public class CancelReservationResponse : IOcppResponse
    {
        [JsonPropertyName("status")] public CancelReservationStatus Status { get; set; }
    }
}
