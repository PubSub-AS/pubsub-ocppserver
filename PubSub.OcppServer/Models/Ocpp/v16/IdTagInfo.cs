using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Models.Ocpp.v16;

public partial class IdTagInfo
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("expiryDate")]
    public DateTimeOffset? ExpiryDate { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("parentIdTag")]
    [JsonConverter(typeof(MinMaxLengthCheckConverter))]
    public string ParentIdTag { get; set; } = string.Empty;

    [JsonPropertyName("status")]
    public AuthorizationStatus Status { get; set; }
    public IdTagInfo() {}
    public IdTagInfo(string parentIdTag)
    {
        ParentIdTag = parentIdTag;
    }
    public IdTagInfo(bool accepted, string parentIdTag)
    {
        if (!accepted)
        {
            ExpiryDate = DateTimeOffset.MinValue;
            Status = AuthorizationStatus.Invalid;
        }
        else
        {
            ExpiryDate = DateTimeOffset.Now + TimeSpan.FromHours(2);
            Status = AuthorizationStatus.Accepted;
        }
        ParentIdTag = parentIdTag;
        
    }
}