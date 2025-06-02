using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Models.Ocpp.v201
{
    public class GetCompositeScheduleResponse : IOcppResponse
    {
   
        public CompositeSchedule? Schedule { get; set; }
        

        public GenericStatusEnum Status { get; set; }
        public StatusInfo? StatusInfo { get; set; }
    }
}