using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Models.Ocpp.v201
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum IdTokenEnum
    {
        Central,
        eMAID,
        ISO14443,
        ISO15693,
        KeyCode,
        Local,
        MacAddress,
        NoAuthorization
    }
}
