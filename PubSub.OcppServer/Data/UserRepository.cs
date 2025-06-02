using PubSub.OcppServer.Data.Interfaces;
using PubSub.OcppServer.Models.EF;

namespace PubSub.OcppServer.Data;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(ChargingContext context) : base(context)
    {

    }
}