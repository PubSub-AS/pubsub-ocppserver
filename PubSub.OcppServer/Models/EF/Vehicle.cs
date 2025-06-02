namespace PubSub.OcppServer.Models.EF
{
    public class Vehicle
    {
        public int VehicleId { get; set; }
        public string VehicleName { get; set;}
        public FacilityOwner FacilityOwner { get; set; }
        public string VehicleManufacturer { get; set; }
        public IdTag IdTag { get; set; }
    }
}
