using PubSub.OcppServer.Data.Interfaces;
using PubSub.OcppServer.Models.EF;

namespace PubSub.OcppServer.Data
{
    public class ReservationRepository : GenericRepository<Reservation>, IReservationRepository
    {
        public ReservationRepository(ChargingContext context) : base(context)
        {

        }
        public int GetFirstAvailableReservationId()
        {
            var reservations = _context
                .Set<Reservation>()
                .ToList();
            if (reservations.Count == 0) return 1;
            return reservations.Max(r => r.ReservationId) + 1;
        }
    }
}
