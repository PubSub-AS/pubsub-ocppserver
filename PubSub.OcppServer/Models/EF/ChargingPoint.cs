namespace PubSub.OcppServer.Models.EF
{
    public class ChargingPoint
    {
        public string? ChargingPointID { get; set; }
        public int FacilityID { get; set; }
        public Facility Facility { get; set; }
        public ICollection<ChargingTransaction> ChargingTransactions { get; set; }
        public int ChargingPointProviderID { get; set; }
        public ChargingPointProvider ChargingPointProvider { get; set; }
        public ICollection<Evse>? Evses { get; set; }
        public ICollection<Connector> Connectors { get; set; } = new List<Connector>();
        public string? State { get; set; }
        public string? ChargePointSerialNumber { get; set; }
        public string? FirmwareVersion { get; set; }
        public string? ChargePointModel { get; set; }
        public ICollection<ApiUser> ApiUsers { get; set; }
        public ChargingPoint(){}

    }
}
