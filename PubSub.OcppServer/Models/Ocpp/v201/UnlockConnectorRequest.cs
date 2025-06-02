using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Models.Ocpp.v201
{
    public partial class UnlockConnectorRequest : IOcppRequest
    {
        public int EvseId { get; set; }
        public int ConnectorId { get; set; }
    }
}