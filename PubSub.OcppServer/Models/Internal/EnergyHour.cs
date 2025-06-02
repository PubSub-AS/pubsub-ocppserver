namespace PubSub.OcppServer.Models.Internal;

public class EnergyHour
{
    public double EurMWh { get; set; }
    public double NokMwh { get; set; }
    public DateTimeOffset Time { get; set; }
}