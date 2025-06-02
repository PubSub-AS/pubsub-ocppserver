namespace PubSub.OcppServer.Models.Ocpp.v201;

public class NotifyCustomerInformationRequest : IOcppRequest
{
    public string Data { get; set; }
    public bool? Tbc { get; set; }
    public int SecNo { get; set; }
}