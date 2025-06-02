using PubSub.OcppServer.Models.Internal;

namespace PubSub.OcppServer.Models.EventArguments
{
    public class GenericArgs : EventArgs
    {
        public string ChargingPointId { get; set; }
        public GenericEventTypeEnum EventType { get; set; }
        public DateTimeOffset Timestamp { get; set; }
    }
}
