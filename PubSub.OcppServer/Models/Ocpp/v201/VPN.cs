namespace PubSub.OcppServer.Models.Ocpp.v201;

public class VPN
{
    public string Server { get; set; }
    public string User { get; set; }
    public string Group { get; set; }
    public string Password { get; set; }
    public string Key { get; set; }
    public VPNEnum Type { get; set; }
}