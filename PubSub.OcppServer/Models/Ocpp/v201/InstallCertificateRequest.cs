namespace PubSub.OcppServer.Models.Ocpp.v201;

public class InstallCertificateRequest : IOcppRequest
{
    public InstallCertificateUseEnum CertificateType { get; set; }
    public string Certificate { get; set; }
}