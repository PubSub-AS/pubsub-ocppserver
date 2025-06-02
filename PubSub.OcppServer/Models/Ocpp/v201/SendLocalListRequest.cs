namespace PubSub.OcppServer.Models.Ocpp.v201
{
    public class SendLocalListRequest : IOcppRequest
    {
        public int VersionNumber { get; set; }
        public AuthorizationData[]? LocalAuthorizationList { get; set; }
        public UpdateEnum UpdateType { get; set; }
    }

}
