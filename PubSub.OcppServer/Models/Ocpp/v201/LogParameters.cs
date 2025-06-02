namespace PubSub.OcppServer.Models.Ocpp.v201;

public class LogParameters
{
    public string RemoteLocation { get; set; }
    public DateTime? OldestTimestamp { get; set; }
    public DateTime? LatestTimestamp { get; set; }
}