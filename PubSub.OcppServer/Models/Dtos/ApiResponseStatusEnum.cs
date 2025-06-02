using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Models.Dtos;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ApiResponseStatusEnum
{
    Accepted,
    ChargingPointNotConnected,
    Faulted,
    MessageUnsupported,
    NotImplemented,
    NotSupported,
    Occupied,
    OK,
    RebootRequired,
    Rejected,
    TransactionUnknown,
    Unavailable,
    Unknown
        
      
}