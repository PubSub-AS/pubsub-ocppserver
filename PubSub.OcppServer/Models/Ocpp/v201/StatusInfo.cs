using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Models.Ocpp.v201;

public class StatusInfo
{
    public string ReasonCode { get; set; }
    public string? AdditionalInfo { get; set; }
}