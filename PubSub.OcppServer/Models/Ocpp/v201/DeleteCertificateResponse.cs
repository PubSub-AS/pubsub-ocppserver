using PubSub.OcppServer.Models.Ocpp.v201;

public class DeleteCertificateResponse
{
    public DeleteCertificateStatusEnum Status { get; set; }
    public StatusInfo? StatusInfo { get; set; }
}