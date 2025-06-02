using PubSub.OcppServer.Data.Interfaces;
using PubSub.OcppServer.Models.EF;

namespace PubSub.OcppServer.Data;

public class ApiUserRepository : GenericRepository<ApiUser>, IApiUserRepository
{
    public ApiUserRepository(ChargingContext context) : base(context)
    {

    }
}