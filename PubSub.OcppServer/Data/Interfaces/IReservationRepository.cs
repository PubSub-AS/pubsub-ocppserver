using PubSub.OcppServer.Models.EF;

namespace PubSub.OcppServer.Data.Interfaces
{
    public interface IReservationRepository : IGenericRepository<Reservation>
    {
        int GetFirstAvailableReservationId();
    }
}
