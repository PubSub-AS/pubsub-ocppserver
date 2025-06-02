namespace PubSub.OcppServer.Models.Internal
{
    public class OptimizedChargingArgs
    {
        public string energyZoneName { get; set; }
        public DateTimeOffset startCharge { get; set; }
        public DateTimeOffset endCharge { get; set; }
        public double neededKwh { get; set; }
        public double chargingEffectKw { get; set; }
    }
}
