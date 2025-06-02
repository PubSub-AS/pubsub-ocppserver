namespace PubSub.OcppServer.Models.EF
{
    public class Facility
    {
        public int FacilityID { get; set; }
        public string FacilityName { get; set; }
        public int FacilityOwnerID { get; set; }
        public FacilityOwner FacilityOwner { get; set; }
        public ICollection<ChargingPoint> ChargingPoints { get; set; }
        public string EnergyZoneName { get; set; }
        public string UserId { get; set; }
        public ApiUser User { get; set; }
    }
}
