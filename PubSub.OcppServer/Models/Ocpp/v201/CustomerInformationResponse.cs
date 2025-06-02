using PubSub.OcppServer.Models.Ocpp.v201;

public class CustomerInformationResponse
{
    public CustomerInformationStatusEnum Status { get; set; }
    public StatusInfo? StatusInfo { get; set; }
}