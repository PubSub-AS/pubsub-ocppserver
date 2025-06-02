namespace PubSub.OcppServer.Models.Ocpp.v201;

public class ReserveNowResponse : IOcppResponse
{
    public ReserveNowStatusEnum Status { get; set; }
    public StatusInfo? StatusInfo { get; set; }
}