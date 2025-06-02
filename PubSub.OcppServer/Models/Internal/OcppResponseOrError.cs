using PubSub.OcppServer.Models.Ocpp.v16;

namespace PubSub.OcppServer.Models.Internal
{
    public class OcppResponseOrError
    {
        public object? OcppResponse { get; set; }
        public string? ErrorCode { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
