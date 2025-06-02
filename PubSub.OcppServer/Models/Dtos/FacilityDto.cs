using PubSub.OcppServer.Models.EF;

namespace PubSub.OcppServer.Models.Dtos
{
    public class FacilityDto
    {
        public int FacilityID { get; set; }
        public string FacilityName { get; set; }
        public string FacilityOwner { get; set; }
        public ICollection<ChargingPointDto> ChargingPoints { get; set; }
        public string EnergyZoneName { get; set; }
        public string User { get; set; }
    }
}
