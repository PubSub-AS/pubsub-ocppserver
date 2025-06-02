namespace PubSub.OcppServer.Models.EF
{
    public class FacilityOwner
    {
        public int FacilityOwnerId { get; set; }
        public string FacilityOwnerName { get; set; }
        public ICollection<Facility> Facilities { get; set; }
    }
}
