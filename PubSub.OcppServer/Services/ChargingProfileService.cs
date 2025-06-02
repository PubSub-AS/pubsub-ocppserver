using PubSub.OcppServer.Adapters;
using PubSub.OcppServer.Models.Internal;
using PubSub.OcppServer.Models.Ocpp.v16;
using PubSub.OcppServer.Services.Interfaces;
using ChargingSchedulePeriod = PubSub.OcppServer.Models.Ocpp.v16.ChargingSchedulePeriod;

namespace PubSub.OcppServer.Services
{
    public class CachedChargingProfile
    {
        public ChargingProfile ChargingProfile { get; set; }
        public DateTimeOffset LastUpdated { get; set; }
    }
    public class ChargingProfileService : IChargingProfileService
    {
        private readonly ILogger<ChargingProfileService> _logger;
        private Dictionary<string, CachedChargingProfile> _cachedChargingProfiles;

        public ChargingProfileService(ILogger<ChargingProfileService> logger)
        {
            _logger = logger;
            _cachedChargingProfiles = new Dictionary<string, CachedChargingProfile>();

        }
        public async Task<ChargingProfile> Create(OptimizedChargingArgs optimizedChargingArgs, string chargingPoint, int connector)
        {
            var chargeHours = new List<DateTimeOffset> { DateTimeOffset.UtcNow };
            chargeHours = chargeHours
                .Order()
                .ToList();
            
            var periods = new List<ChargingSchedulePeriod>();
            bool previousState = false; // Start med at det ikke lades
            DateTimeOffset? previousTime = null;
            var scheduleStart = chargeHours.First();
            foreach (var time in chargeHours)
            {
                bool currentState = true; // Vi antar at hvert tidspunkt betyr at det lades

                // Sjekk for langt gap
                if (previousTime.HasValue && (time - previousTime.Value).TotalHours > 1)
                {
                    // Legg til "Stop" for å avslutte lading
                    if (previousState) periods.Add(CreatePeriod(scheduleStart, previousTime.Value.AddHours(1), false));
                    
                    // Legg til "Start" for å starte ny lading
                    if (currentState) periods.Add(CreatePeriod(scheduleStart, time, true));
                }
                else if (currentState != previousState)
                {
                    periods.Add(CreatePeriod(scheduleStart, currentState ? time : previousTime.Value.AddHours(1),
                        currentState));
                }
                previousState = currentState;
                previousTime = time;
            }
            // Legg til en "Stop"-handling hvis siste tilstand er lading
            if (previousState) periods.Add(CreatePeriod(scheduleStart, previousTime.Value.AddHours(1), false));
            

            var schedule = new ChargingSchedule(
                ChargingRateUnit.W,
                periods.ToArray(),
                null,
                null, 
                scheduleStart);
            var profile = new ChargingProfile(
                12345, // placeholder
                ChargingProfileKind.Absolute,
                ChargingProfilePurpose.TxProfile,
                schedule,
                null,
                0,
                null,
                chargeHours.First(),
                chargeHours.Last() + TimeSpan.FromHours(1)
            );
    
            CachedChargingProfile cachedChargingProfile = new()
            {
                ChargingProfile = profile,
                LastUpdated = DateTimeOffset.UtcNow
            };
            var cacheKey = GetChargingProfileCacheKey(chargingPoint, connector);
            if (_cachedChargingProfiles.ContainsKey(cacheKey)) _cachedChargingProfiles.Remove(cacheKey);
            _cachedChargingProfiles.Add(cacheKey, cachedChargingProfile);
            return profile;
        }

        public ChargingProfile? Get(string chargingPoint, int connector)
        {
            var cacheKey = GetChargingProfileCacheKey(chargingPoint, connector);
            var success = _cachedChargingProfiles.TryGetValue(cacheKey, out var profile);
            if (!success) return null;
            if (DateTimeOffset.UtcNow - profile.LastUpdated > TimeSpan.FromMinutes(1)) return null;
            return profile.ChargingProfile;
        }

        private string GetChargingProfileCacheKey(string chargingPoint, int connector)
        {
            return chargingPoint + connector.ToString("000");
        }

        private ChargingSchedulePeriod CreatePeriod(DateTimeOffset scheduleStart, DateTimeOffset eventTime, bool isStarting)
        {
            return new ChargingSchedulePeriod()
            {
                Limit = isStarting ? 1000000 : 0,
                NumberPhases = null,
                StartPeriod = (long)(eventTime - scheduleStart).TotalSeconds
            };

        }
     
    }
}
