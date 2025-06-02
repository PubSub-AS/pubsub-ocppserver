namespace PubSub.OcppServer.Models.Ocpp.v201;

public class UnpublishFirmwareResponse : IOcppResponse 
{
    public UnpublishFirmwareStatusEnum Status { get; set; }
}