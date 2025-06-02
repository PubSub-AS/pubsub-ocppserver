using System.Text.Json;
using PubSub.OcppServer.Models.Dtos;
using PubSub.OcppServer.Models.Internal;
using PubSub.OcppServer.Services;

namespace PubSub.OcppServer.Adapters
{
    public class EnergyPriceAdapter : IEnergyPriceAdapter
    {
        private readonly ILogger<EnergyPriceAdapter> _logger;

        public EnergyPriceAdapter(ILogger<EnergyPriceAdapter> logger)
        {
            _logger = logger;
        }

        public async Task<IEnumerable<EnergyHour>> FetchDailyPricesAsync(DateOnly date, string energyZoneName)
        {

            var uri = new Uri($"https://your-energy-api/{energyZoneName}/{date.ToString("yyyy-MM-dd")}");
            var httpClient = new HttpClient();
            var httpResponse = await httpClient.GetAsync(uri);
            if (!httpResponse.IsSuccessStatusCode)
            {
                _logger.LogInformation("Rejected by the Energy price API!");
                return new List<EnergyHour>();
            }

            var responseString = await httpResponse.Content.ReadAsStringAsync();
               
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new ZuluTimeConverter() }
            };
            var energyDay = JsonSerializer.Deserialize<EnergyPriceDto>(responseString, options);

            var energyPriceHours = energyDay.EnergyPriceHours;

            if (energyPriceHours == null)
            {
                _logger.LogInformation("Got empty data from energy price API");
                return new List<EnergyHour>();
            }

            var energyHours = new List<EnergyHour>();
            foreach (var energyPriceHour in energyPriceHours)
            {
                energyHours.Add(new EnergyHour()
                {
                    EurMWh = energyPriceHour.PriceEur,
                    NokMwh = energyPriceHour.PriceNok,
                    Time = energyPriceHour.UtcHour
                });
            }

            return energyHours;
        }

        public double GetCurrentPriceMwhEuros(string energyZoneName)
        {
            var todaysPrices = FetchDailyPricesAsync(DateOnly.FromDateTime(DateTime.Now), energyZoneName.ToString())
                .Result;
            var energyHour = todaysPrices
                .FirstOrDefault(h => h.Time < DateTimeOffset.Now);

            return energyHour?.EurMWh ?? 0;
        }
    }

}
