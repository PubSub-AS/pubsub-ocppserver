using PubSub.OcppServer.Models.EF;

namespace PubSub.OcppServer.Models.Dtos
{
    public record MeterValueDto
    {
        public string MeterValueID { get; set; }
        public string ChargingTransactionID { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public double? ValueRaw { get; set; }
        public string? ValueSignedData { get; set; }
        public string? Unit { get; set; }
        public string? Context { get; set; }
        public string? Location { get; set; }
        public string? Measurand { get; set; }
        public string? Phase { get; set; }
  
    }
}
