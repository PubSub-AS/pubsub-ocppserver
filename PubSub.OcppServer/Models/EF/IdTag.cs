namespace PubSub.OcppServer.Models.EF
{
    public class IdTag
    {
        public string IdTagID { get; set; }
        public string UserId { get; set; }
        public ApiUser User { get; set; }
        public ICollection<ChargingTransaction> ChargingTransactions { get; set; }
    }
}
