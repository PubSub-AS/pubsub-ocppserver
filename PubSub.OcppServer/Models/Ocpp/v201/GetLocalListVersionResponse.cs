using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Models.Ocpp.v201
{
    public class GetLocalListVersionResponse : IOcppResponse
    {
       
        public int VersionNumber { get; set; }
    }

}