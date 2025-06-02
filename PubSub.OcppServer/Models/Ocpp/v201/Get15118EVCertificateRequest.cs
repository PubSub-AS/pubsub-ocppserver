namespace PubSub.OcppServer.Models.Ocpp.v201;

public class Get15118EVCertificateRequest : IOcppRequest
{
    public string Iso15118SchemaVersion { get; set; }
    public CertificateActionEnum Action { get; set; }
    public string ExiRequest { get; set; }
}