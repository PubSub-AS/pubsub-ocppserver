using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Models.Ocpp.v201
{
    public class ResetRequest : IOcppRequest
    {
       public ResetEnum Type { get; set; }
    }
}
