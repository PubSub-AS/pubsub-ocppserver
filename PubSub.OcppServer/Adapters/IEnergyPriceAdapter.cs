using PubSub.OcppServer.Models.Dtos;
using PubSub.OcppServer.Models.Internal;

namespace PubSub.OcppServer.Adapters
{
    public interface IEnergyPriceAdapter
    {
        Task<IEnumerable<EnergyHour>> FetchDailyPricesAsync(DateOnly date, string energyZoneName);
        double GetCurrentPriceMwhEuros(string energyZoneName);

    }
}
