using PubSub.OcppServer.Models.EF;
using System.Security.Claims;

namespace PubSub.OcppServer.Data.Interfaces
{
    public interface IChargingTransactionRepository : IGenericRepository<ChargingTransaction>
    {
        int GetFirstAvailablev16Id();
        IEnumerable<ChargingTransaction> GetRecentTransactions();
        Task<ChargingTransaction?> GetTransactionAsync(string transactionId);
        ChargingTransaction GetChargingTransactionWithEnergyZoneName(string transactionId);

    }
}