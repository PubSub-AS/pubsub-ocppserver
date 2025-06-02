namespace PubSub.OcppServer.Models.Ocpp.v201
{
    public class CostUpdatedRequest : IOcppRequest
    {
        public decimal TotalCost { get; set; }
        public string TransactionId { get; set; }
    }


}