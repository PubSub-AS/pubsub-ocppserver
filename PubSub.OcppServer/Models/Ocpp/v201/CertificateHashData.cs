namespace PubSub.OcppServer.Models.Ocpp.v201;

public class CertificateHashData
{
    public HashAlgorithmEnum HashAlgorithm { get; set; }
    public string IssuerNameHash { get; set; }
    public string IssuerKeyHash { get; set; }
    public string SerialNumber { get; set; }
}