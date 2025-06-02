using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Models.Ocpp.v201
{
    public class MeterValue
    {
        public SampledValue[] SampledValue { get; set; }
        public DateTimeOffset Timestamp { get; set; }
    }
}
