using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Models.Ocpp.v16
{
    public class RemoteStopTransactionResponse : IOcppResponse 
    {
        [JsonPropertyName("status")] public RemoteStartStopStatus Status { get; set; }
    }
}