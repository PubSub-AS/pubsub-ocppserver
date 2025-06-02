namespace PubSub.OcppServer.Models.Ocpp.v201;

public class GetInstalledCertificateIdsResponse : IOcppResponse
{
    public GetInstalledCertificateStatusEnum status { get; set; }
    public CertificateHashDataChain? CertificateHashDataChain { get; set; }
    public StatusInfo? StatusInfo { get; set; }
}