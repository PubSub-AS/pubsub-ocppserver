using PubSub.OcppServer.Data.Interfaces;
using PubSub.OcppServer.Models.EF;

namespace PubSub.OcppServer.Data
{
    public class IdTagRepository : GenericRepository<IdTag>, IIdTagRepository
    {
        public IdTagRepository(ChargingContext context) : base(context)
        {

        }
    }
}
