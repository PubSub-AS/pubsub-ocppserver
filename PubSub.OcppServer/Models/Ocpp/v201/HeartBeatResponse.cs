using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Models.Ocpp.v201
{
    public class HeartbeatResponse : IOcppResponse
    {
       
        public DateTimeOffset CurrentTime { get; set; }
        public HeartbeatResponse()
        {
            CurrentTime = DateTimeOffset.Now;
        }
    }
}