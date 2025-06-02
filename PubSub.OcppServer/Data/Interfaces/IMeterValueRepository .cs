using PubSub.OcppServer.Models.EF;

namespace PubSub.OcppServer.Data.Interfaces
{
    public interface IMeterValueRepository : IGenericRepository<MeterValue>
    {
        List<MeterValue> GetByTransactionId(string id);
    }
}
