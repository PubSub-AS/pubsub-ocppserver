using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Models.Ocpp.v201
{
    public partial class AuthorizeResponse : IOcppResponse
    {
     
        public AuthorizeCertificateStatusEnum? CertificateStatus { get; set; }
        public IdTokenInfo IdTokenInfo { get; set; }
    }
}
