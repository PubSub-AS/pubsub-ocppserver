namespace PubSub.OcppServer.Models.Ocpp.v201;

public class PublishFirmwareStatusNotificationRequest : IOcppRequest
{
    public PublishFirmwareStatusEnum Status { get; set; }
    public string? Location { get; set; }
    public int? RequestId { get; set; }
}