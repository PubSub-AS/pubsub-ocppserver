using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Models.Ocpp.v201
{
    public class DataTransferRequest : IOcppRequest
    {
        public object? Data { get; set; }
        public string? MessageId { get; set; }
        public string VendorId { get; set; }
    }
}