namespace PubSub.OcppServer.Models.Ocpp.v201;

public class Firmware
{
    public int RequestId { get; set; }
    public DateTime RetrieveDateTime { get; set; }
    public DateTime? InstallDateTime { get; set; }
    public string? SigningCertificate { get; set; }
    public string? Signature { get; set; }
}