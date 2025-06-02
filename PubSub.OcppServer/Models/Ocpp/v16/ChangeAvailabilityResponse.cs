using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Models.Ocpp.v16
{
    public partial class ChangeAvailabilityResponse : IOcppResponse
    {
        [JsonPropertyName("status")] public AvailabilityStatus Status { get; set; }
    }
}