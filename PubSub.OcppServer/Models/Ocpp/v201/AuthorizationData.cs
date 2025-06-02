using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Models.Ocpp.v201
{
    public partial class AuthorizationData
    {
        public IdTokenInfo? IdTokenInfo { get; set; }
        public IdTokenType IdToken { get; set; }
    }
}
