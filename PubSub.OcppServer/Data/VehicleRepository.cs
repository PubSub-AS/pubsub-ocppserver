using PubSub.OcppServer.Data.Interfaces;
using PubSub.OcppServer.Models.EF;

namespace PubSub.OcppServer.Data
{
    public class VehicleRepository : GenericRepository<Vehicle>, IVehicleRepository
    {
        public VehicleRepository(ChargingContext context) : base(context)
        {

        }
    }
}
