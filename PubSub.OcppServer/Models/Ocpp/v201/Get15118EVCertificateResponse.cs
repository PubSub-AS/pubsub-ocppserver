namespace PubSub.OcppServer.Models.Ocpp.v201;

public class Get15118EVCertificateResponse
{
    public Iso15118EVCertificateStatusEnum Status { get; set; }
    public string ExiResponse { get; set; }
    public StatusInfo? StatusInfo { get; set; }
}