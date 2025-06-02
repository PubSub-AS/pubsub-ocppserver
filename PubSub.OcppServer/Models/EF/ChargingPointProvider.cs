namespace PubSub.OcppServer.Models.EF
{
    public class ChargingPointProvider
    {
        public int ChargingPointProviderID { get; set; }
        public string ChargingPointProviderName { get; set; }
        public ICollection<ChargingPoint>? ChargingPoints { get; set; }
    }
}
