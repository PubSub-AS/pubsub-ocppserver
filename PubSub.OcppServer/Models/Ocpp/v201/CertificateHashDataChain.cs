namespace PubSub.OcppServer.Models.Ocpp.v201;

public class CertificateHashDataChain
{
    public GetCertificateIdUseEnum CertificateType { get; set; }
    public CertificateHashData CertificateHashData { get; set; }
    public CertificateHashData[]? ChildCertificateHashData { get; set; }
}