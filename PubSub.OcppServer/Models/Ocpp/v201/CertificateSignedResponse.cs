namespace PubSub.OcppServer.Models.Ocpp.v201;

public class CertificateSignedResponse : IOcppResponse
{
    public CertificateSignedStatusEnum Status { get; set; }
    public StatusInfo? StatusInfo { get; set; }
}