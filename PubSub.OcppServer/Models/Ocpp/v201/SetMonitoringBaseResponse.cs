namespace PubSub.OcppServer.Models.Ocpp.v201;

public class SetMonitoringBaseResponse : IOcppResponse
{
    public GenericDeviceModelStatusEnum Status { get; set; }
    public StatusInfo? StatusInfo { get; set; }
}