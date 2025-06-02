using PubSub.OcppServer.Data.Interfaces;
using PubSub.OcppServer.Models.EF;

namespace PubSub.OcppServer.Data
{
    public class ChargingPointProviderRepository : GenericRepository<ChargingPointProvider>, IChargingPointProviderRepository
    {
        public ChargingPointProviderRepository(ChargingContext context) : base(context)
        {

        }
    }
}
