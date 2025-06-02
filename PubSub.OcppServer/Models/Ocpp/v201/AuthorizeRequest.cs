using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Models.Ocpp.v201
{
    public class AuthorizeRequest : IOcppRequest
    {
        public string? Certificate { get; set; }
        public IdTokenType IdToken { get; set; }
        public OCSPRequestData? Iso15118CertificateHashData { get; set; }
    }
}