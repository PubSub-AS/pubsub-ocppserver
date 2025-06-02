using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Models.Ocpp.v16
{
    public class SendLocalListRequest : IOcppRequest
    {
      

        [JsonPropertyName("listVersion")] public long ListVersion { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("localAuthorizationList")]
        public LocalAuthorizationList[] LocalAuthorizationList { get; set; }

        [JsonPropertyName("updateType")] public UpdateType UpdateType { get; set; }
    }
}
