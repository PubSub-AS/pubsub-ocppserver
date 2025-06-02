namespace PubSub.OcppServer.Models.Ocpp.v201;

public class CustomerInformationRequest : IOcppRequest
{
    public int RequestId { get; set; }
    public bool Report { get; set; }
    public bool Clear { get; set; }
    public string? CustomerIdentifier { get; set; }
    public IdTokenType? IdToken { get; set; }
    public CertificateHashData? CustomerCertificate { get; set; }
}