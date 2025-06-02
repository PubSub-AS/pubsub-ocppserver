using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Models.Ocpp.v201
{
 
    public enum UnlockStatusEnum
    {
        Unlocked,
        UnlockFailed,
        OngoingAuthorizedTransaction,
        UnknownConnector
    }
}
