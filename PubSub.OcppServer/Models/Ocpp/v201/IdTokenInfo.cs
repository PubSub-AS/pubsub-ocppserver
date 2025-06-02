using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Models.Ocpp.v201;

public class IdTokenInfo
{
    public AuthorizationStatusEnum Status { get; set; }
    public DateTime? CacheExpiryDateTime { get; set; }
    public int? ChargingPriority { get; set; }
    public string? Language1 { get; set; }
    public string? Language2 { get; set; }
    public List<int>? EvseId { get; set; }
    public IdTokenType GroupIdToken { get; set; }
    public MessageContent PersonalMessage { get; set; }


}