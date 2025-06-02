namespace PubSub.OcppServer.Models.EF
{
    public class User
    {
        // Note that the ApiUser and ChargePointUser classes are inheriting from this one
        public string UserId { get; set; }
        public string HashedPassword { get; set; }
        public string Salt { get; set; }
        
    }
}
