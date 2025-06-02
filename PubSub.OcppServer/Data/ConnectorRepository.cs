using PubSub.OcppServer.Data.Interfaces;
using PubSub.OcppServer.Models.EF;

namespace PubSub.OcppServer.Data
{
    public class ConnectorRepository : GenericRepository<Connector>, IConnectorRepository
    {
        public ConnectorRepository(ChargingContext context) : base(context)
        {

        }
    }
}
