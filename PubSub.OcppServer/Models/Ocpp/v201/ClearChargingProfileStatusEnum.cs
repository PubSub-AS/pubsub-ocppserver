using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Models.Ocpp.v201
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ClearChargingProfileStatusEnum
    {
        Accepted,
        Unknown
    }
}
