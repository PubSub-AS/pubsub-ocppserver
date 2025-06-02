namespace PubSub.OcppServer.Models.Ocpp.v201;

public class GetCertificateStatusRequest : IOcppRequest
{
    public OCSPRequestData OcspRequestData { get; set; }
}