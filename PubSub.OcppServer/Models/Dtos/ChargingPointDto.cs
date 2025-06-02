using PubSub.OcppServer.Models.EF;

namespace PubSub.OcppServer.Models.Dtos
{
    public class ChargingPointDto
    {
        public string ChargingPointID { get; set; }
        public string Facility { get; set; }
        public string Model { get; set; }
        public string State { get; set; }
    }
}
