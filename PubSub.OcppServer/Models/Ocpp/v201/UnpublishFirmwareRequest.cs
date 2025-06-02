namespace PubSub.OcppServer.Models.Ocpp.v201;

public class UnpublishFirmwareRequest : IOcppRequest 
{
    public string Checksum { get; set; }
}