using PubSub.OcppServer.Data.Interfaces;
using PubSub.OcppServer.Models.EF;

namespace PubSub.OcppServer.Data
{
    public class EvseRepository : GenericRepository<Evse>, IEvseRepository
    {
        public EvseRepository(ChargingContext context) : base(context) { }
    }
}
