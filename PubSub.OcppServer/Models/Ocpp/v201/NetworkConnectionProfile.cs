namespace PubSub.OcppServer.Models.Ocpp.v201;

public class NetworkConnectionProfile
{
    public OCPPVersionEnum OcppVersion { get; set; }
    public OCPPTransportEnum OcppTransport { get; set; }
    public string OcppCsmsUrl { get; set; }
    public int MessageTimeout { get; set; }
    public int SecurityProfile { get; set; }
    public OCPPInterfaceEnum OcppInterface { get; set; }
    public VPN Vpn { get; set; }
    public APN Apn { get; set; }
}