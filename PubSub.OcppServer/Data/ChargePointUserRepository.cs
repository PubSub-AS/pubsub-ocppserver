using PubSub.OcppServer.Data.Interfaces;
using PubSub.OcppServer.Models.EF;

namespace PubSub.OcppServer.Data;

public class ChargePointUserRepository : GenericRepository<ChargePointUser>, IChargePointUserRepository
{
    public ChargePointUserRepository(ChargingContext context) : base(context)
    {

    }
}