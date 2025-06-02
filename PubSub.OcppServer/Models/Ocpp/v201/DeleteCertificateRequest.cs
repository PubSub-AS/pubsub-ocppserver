using PubSub.OcppServer.Models.Ocpp;
using PubSub.OcppServer.Models.Ocpp.v201;

public class DeleteCertificateRequest : IOcppRequest
{
    public CertificateHashData CertificateHashData { get; set; }
}