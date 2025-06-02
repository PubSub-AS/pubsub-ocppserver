using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using PubSub.OcppServer.Data.Interfaces;
using PubSub.OcppServer.Models.EF;

namespace PubSub.OcppServer.Data
{
    public class ChargingTransactionRepository : GenericRepository<ChargingTransaction>, IChargingTransactionRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _email;
        private readonly bool _isAdmin;

        public ChargingTransactionRepository(
            ChargingContext context,
            IHttpContextAccessor httpContextAccessor) : base(context)
        {
            _httpContextAccessor = httpContextAccessor;
            _email = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Email);
            _isAdmin = _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == "IsAdmin")?.Value == "True";

        }

        public int GetFirstAvailablev16Id()
        { 
            var transactions = _context
                .Set<ChargingTransaction>()
                .ToList();
            if (transactions.Count == 0) return 1;
            return transactions.Max(t => t.v16Id) + 1;
        }

        public async Task<ChargingTransaction?> GetTransactionAsync(string transactionId)
        {
         
            var transaction = await _context.ChargingTransactions
                //.AsNoTracking()
                .Include(t => t.MeterValues.OrderBy(mv => mv.Timestamp))
                .Where(t => t.IdTag.User.Email == _email || _isAdmin)
                .FirstOrDefaultAsync(t => t.ChargingTransactionID == transactionId);
            if (transaction?.TotalKWh == null)
            {
                transaction.CalculateTotalKWh();
            }
            if (transaction?.TotalSeconds == null)
            {
                transaction.CalculateTotalSeconds();
            }

            if (transaction.TotalPriceEuros == null)
            {
                transaction.CalculatePrice();
            }

            await _context.SaveChangesAsync();
            return transaction;
        }

        public IEnumerable<ChargingTransaction> GetRecentTransactions()
        {
            var transactions = _context
                .ChargingTransactions
                .AsNoTracking()
                .Where(t => t.IdTag.User.Email == _email
                || _isAdmin);

            return transactions;
        }

        public ChargingTransaction GetChargingTransactionWithEnergyZoneName(string transactionId)
        {
            var transaction = _context.ChargingTransactions
                .Include(t => t.ChargingPoint)
                .ThenInclude(cp => cp.Facility)
                .FirstOrDefault(t => t.ChargingTransactionID == transactionId);
            return transaction;
        }

      
    }
}
