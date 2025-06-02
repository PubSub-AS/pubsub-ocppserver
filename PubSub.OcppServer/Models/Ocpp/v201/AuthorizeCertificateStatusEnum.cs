namespace PubSub.OcppServer.Models.Ocpp.v201;

public enum AuthorizeCertificateStatusEnum
{
    Accepted,
    SignatureError,
    CertificateExpired,
    CertificateRevoked,
    NoCertificateAvailable,
    CertChainError,
    ContractCancelled
}