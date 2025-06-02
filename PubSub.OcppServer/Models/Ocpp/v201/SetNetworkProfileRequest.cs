namespace PubSub.OcppServer.Models.Ocpp.v201;

public class SetNetworkProfileRequest : IOcppRequest
{
    public int ConfigurationSlot { get; set; }
    public NetworkConnectionProfile ConnectionData { get; set; }
}