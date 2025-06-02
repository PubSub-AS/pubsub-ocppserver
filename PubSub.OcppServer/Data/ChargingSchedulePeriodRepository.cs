using PubSub.OcppServer.Data.Interfaces;
using PubSub.OcppServer.Models.EF;

namespace PubSub.OcppServer.Data
{
    public class ChargingSchedulePeriodRepository : GenericRepository<ChargingSchedulePeriod>, IChargingSchedulePeriodRepository
    {
        public ChargingSchedulePeriodRepository(ChargingContext context) : base(context)
        {

        }
    }
}
