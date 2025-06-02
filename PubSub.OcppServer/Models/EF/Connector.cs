using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PubSub.OcppServer.Models.Ocpp.v16;

namespace PubSub.OcppServer.Models.EF
{
    public class Connector
    {
        [Key]
        public int ConnectorId { get; set; }
     
        public int ConnectorName { get; set; }
        public int? EvseId { get; set; }
        public Evse? Evse { get; set; }
        public string ChargingPointId { get; set; }
        public ChargingPoint ChargingPoint { get; set; }
        public string State { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
        public ICollection<ChargingTransaction>? ChargingTransactions { get; set; }

    }
}
