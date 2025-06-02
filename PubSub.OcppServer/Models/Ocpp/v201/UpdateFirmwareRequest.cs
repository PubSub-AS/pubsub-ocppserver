using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Models.Ocpp.v201
{
    public class UpdateFirmwareRequest : IOcppRequest
    {
   
        public int RequestId { get; set; }
        public int? Retries { get; set; }
        public int? RetryInterval { get; set; }
        public Firmware Firmware { get; set; }
    }
}