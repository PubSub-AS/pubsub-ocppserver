using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Models.Ocpp.v201
{
    public class StatusNotificationRequest : IOcppRequest
    {
        public DateTimeOffset? Timestamp { get; set; }
        public ConnectorStatusEnum ConnectorStatus { get; set; }
        public int EvseId { get; set; }
        public int ConnectorId { get; set; }
        
    }
}
