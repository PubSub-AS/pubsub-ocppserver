using System.ComponentModel.DataAnnotations.Schema;

namespace PubSub.OcppServer.Models.EF
{
    public class Reservation
    {
        public int ReservationId { get; set; }
        public ChargingPoint ChargingPoint { get; set; }
        public string ChargingPointId { get; set; }
        public Connector Connector { get; set; }
        public int ConnectorId { get; set; }
        public DateTimeOffset ExpiryDate { get; set; }
        public IdTag IdTag { get; set; }
        public string IdTagId { get; set; }
        
    }
}
