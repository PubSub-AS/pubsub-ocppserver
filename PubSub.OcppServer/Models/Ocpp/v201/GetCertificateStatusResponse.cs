namespace PubSub.OcppServer.Models.Ocpp.v201;

public class GetCertificateStatusResponse : IOcppResponse
{
    public GetCertificateStatusEnum Status { get; set; }
    public string? OcspResult { get; set; }
    public StatusInfo? StatusInfo { get; set; }
}