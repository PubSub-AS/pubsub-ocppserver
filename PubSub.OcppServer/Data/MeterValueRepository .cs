using PubSub.OcppServer.Data.Interfaces;
using PubSub.OcppServer.Models.EF;

namespace PubSub.OcppServer.Data
{
    public class MeterValueRepository : GenericRepository<MeterValue>, IMeterValueRepository
    {
        public MeterValueRepository(ChargingContext context) : base(context)
        {

        }
        public List<MeterValue> GetByTransactionId(string id)
        {
            return _context
                .Set<MeterValue>()
                .AsQueryable()
                .Where(m => m.ChargingTransactionID == id)
                .ToList();
        }
    }
}
