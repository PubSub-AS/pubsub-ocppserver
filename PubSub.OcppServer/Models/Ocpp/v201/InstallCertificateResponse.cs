namespace PubSub.OcppServer.Models.Ocpp.v201;

public class InstallCertificateResponse : IOcppResponse
{
    public InstallCertificateStatusEnum Status { get; set; }
    public StatusInfo? StatusInfo { get; set; }
}