using PubSub.OcppServer.Data.Interfaces;
using PubSub.OcppServer.Models.EF;

namespace PubSub.OcppServer.Data;

public class FacilityOwnerRepository : GenericRepository<FacilityOwner>, IFacilityOwnerRepository
{
    public FacilityOwnerRepository(ChargingContext context) : base(context)
    {

    }
}