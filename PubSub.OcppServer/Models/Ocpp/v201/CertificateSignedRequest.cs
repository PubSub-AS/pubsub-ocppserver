namespace PubSub.OcppServer.Models.Ocpp.v201
{
    public class CertificateSignedRequest : IOcppRequest
    {
        public string CertificateChain { get; set; }

        public CertificateSigningUseEnum? CertificateType { get; set; }
    }
}
