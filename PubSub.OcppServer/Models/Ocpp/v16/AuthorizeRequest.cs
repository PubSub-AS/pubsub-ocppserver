using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Models.Ocpp.v16
{
    public class AuthorizeRequest : IOcppRequest
    {
        [JsonConverter(typeof(MinMaxLengthCheckConverter))]
        public string? IdTag { get; set; }
    }
}