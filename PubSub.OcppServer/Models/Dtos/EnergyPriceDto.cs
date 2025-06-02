using PubSub.OcppServer.Models.EF;

namespace PubSub.OcppServer.Models.Dtos
{
    public class EnergyPriceDto
    {
        public string EnergyZoneName { get; set; }
        public string EnergyDay { get; set; }
        public List<EnergyPriceHour> EnergyPriceHours { get; set; }
        
    }
    public class EnergyPriceHour
    {
        public DateTimeOffset UtcHour { get; set; }
        public double PriceEur { get; set; }
        public double PriceNok { get; set; }

    }
}
