namespace PubSub.OcppServer.Models.Ocpp.v201
{
    public class UpdateFirmwareResponse : IOcppResponse
    {
        public UpdateFirmwareStatusEnum Status { get; set; }
        public StatusInfo StatusInfo { get; set; }
    }
}

