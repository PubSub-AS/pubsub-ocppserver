using PubSub.OcppServer.Models.EF;

namespace PubSub.OcppServer.Data.Interfaces
{
    public interface IFacilityRepository : IGenericRepository<Facility>
    {
        List<Facility> GetAuthorizedFacilities();
    }
}
