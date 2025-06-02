using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Models.Ocpp.v16
{
    public partial class AuthorizeResponse : IOcppResponse
    {
        public AuthorizeResponse()
        {
            IdTagInfo = new IdTagInfo();
        }


        [JsonPropertyName("idTagInfo")]
        public IdTagInfo IdTagInfo { get; set; }
    }
}
