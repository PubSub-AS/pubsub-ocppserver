using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Models.Ocpp.v16
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum DataTransferStatus
    {
        Accepted,
        Rejected,
        UnknownMessageId,
        UnknownVendorId
    }
}
