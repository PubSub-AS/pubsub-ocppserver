using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Models.Ocpp.v16
{
    public partial class ReserveNowResponse : IOcppResponse
    {
        [JsonPropertyName("status")] public ReservationStatus Status { get; set; }
    }
}