namespace PubSub.OcppServer.Models.EF
{
    public class Evse
    {
        public int Id { get; set; }
        public int EvseId { get; set; }
        public string ChargingPointId { get; set; }
        public ChargingPoint ChargingPoint { get; set; }
        public ICollection<Connector> Connectors { get; set; }
    }
}
