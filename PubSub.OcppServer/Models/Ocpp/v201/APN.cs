namespace PubSub.OcppServer.Models.Ocpp.v201;

public class APN
{
    public string Apn { get; set; }
    public string? ApnUserName { get; set; }
    public string? ApnPassword { get; set; }
    public int? SimPin { get; set; }
    public string? PreferredNetwork { get; set; }
}