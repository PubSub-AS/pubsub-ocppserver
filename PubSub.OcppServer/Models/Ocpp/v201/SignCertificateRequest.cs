namespace PubSub.OcppServer.Models.Ocpp.v201;

public class SignCertificateRequest : IOcppRequest
{
    public string Csr { get; set; }
    public CertificateSigningUseEnum CertificateType { get; set; }
}