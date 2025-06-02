namespace PubSub.OcppServer.Models.EF
{
    public class ChargingSchedulePeriod
    {
        public string ChargingSchedulePeriodId { get; set; }
        public string ChargingTransactionId { get; set; }
        public ChargingTransaction ChargingTransaction { get; set; }
        public DateTimeOffset StartTime { get; set; }
        public bool Charging { get; set;}

        public ChargingSchedulePeriod()
        {

        }
    }
}
