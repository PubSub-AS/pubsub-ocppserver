namespace PubSub.OcppServer.Models.Ocpp.v201;

public class GetInstalledCertificateIdsRequest : IOcppRequest
{
    public GetCertificateIdUseEnum? CertificateType { get; set; }
}