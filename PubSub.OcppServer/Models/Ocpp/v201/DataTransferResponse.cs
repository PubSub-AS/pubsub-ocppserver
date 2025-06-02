using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Models.Ocpp.v201
{
    public class DataTransferResponse : IOcppResponse
    {
   
        public object? Data { get; set; }
        public DataTransferStatusEnum Status { get; set; }
        public StatusInfo? StatusInfo { get; set; }
    }
    

}
