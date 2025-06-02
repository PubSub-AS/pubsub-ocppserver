using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Models.Ocpp.v201
{
    public class ChargingStation
    {
        [JsonPropertyName("serialNumber")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? SerialNumber { get; set; }
        [JsonPropertyName("model")] 
        public string Model { get; set; }
        [JsonPropertyName("modem")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Modem? Modem { get; set; }
        [JsonPropertyName("vendorName")] 
        public string VendorName { get; set; }
        [JsonPropertyName("firmwareVersion")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? FirmwareVersion { get; set; }
    }

    public class Modem
    {
        [JsonPropertyName("iccid")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Iccid { get; set; }

        [JsonPropertyName("imsi")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)] 
        public string? Imsi { get; set; }
    }
}
