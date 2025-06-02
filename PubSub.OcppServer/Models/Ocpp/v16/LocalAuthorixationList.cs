using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Models.Ocpp.v16
{
    public partial class LocalAuthorizationList
    {
        public LocalAuthorizationList(string idTag, v201.IdTokenInfo idTagInfo)
        {
            IdTag = idTag;
            IdTagInfo = idTagInfo;
        }

        [JsonPropertyName("idTag")]
        [JsonConverter(typeof(MinMaxLengthCheckConverter))]
        public string IdTag { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("idTagInfo")]
        public v201.IdTokenInfo IdTagInfo { get; set; }
    }
}
