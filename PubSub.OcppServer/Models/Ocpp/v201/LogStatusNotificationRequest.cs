namespace PubSub.OcppServer.Models.Ocpp.v201;

public class LogStatusNotificationRequest : IOcppRequest
{
    public UploadLogStatusEnum Status { get; set; }
    public int RequestId { get; set; }
}