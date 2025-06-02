using PubSub.OcppServer.Models.Internal;

namespace PubSub.OcppServer.Services
{
    public class OcppHandlerContext
    {
        public string ChargingPointId { get; set; }
        public List<ConnectorInternal> Connectors { get; set; } = new List<ConnectorInternal>();
    }
}
