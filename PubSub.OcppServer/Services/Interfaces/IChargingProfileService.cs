using PubSub.OcppServer.Models.Internal;
using PubSub.OcppServer.Models.Ocpp.v16;

namespace PubSub.OcppServer.Services.Interfaces
{
    public interface IChargingProfileService
    {
        Task<ChargingProfile> Create(OptimizedChargingArgs optimizedChargingArgs, string chargingPoint, int connector);
        ChargingProfile? Get(string chargingPoint, int connector);
        //Task<bool> AreChargingProfilesEnabled();
    }
}
