using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Models.Ocpp.v201
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum AuthorizationStatusEnum
    {
        Accepted,
        Blocked,
        Expired,
        Invalid,
        NoCredit,
        MotAllowedTypeEVSE,
        NotAtThisLocation,
        NotAtThisTime,
        Unknown,
        ConcurrentTx
    };
    
}
