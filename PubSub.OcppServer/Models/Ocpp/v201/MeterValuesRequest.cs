namespace PubSub.OcppServer.Models.Ocpp.v201;

public class MeterValuesRequest : IOcppRequest
{
    public int EvseId { get; set; }
    public MeterValue[] Metervalue { get; set; }
}