namespace PubSub.OcppServer.Models.Ocpp.v201;

public class PublishFirmwareRequest : IOcppRequest
{
    public string Location { get; set; }
    public int? Retries { get; set; }
    public string Checksum { get; set; }
    public int RequestId { get; set; }
    public int? RetryInterval { get; set; }
}