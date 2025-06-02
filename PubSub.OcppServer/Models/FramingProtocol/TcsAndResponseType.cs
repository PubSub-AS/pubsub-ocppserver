using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PubSub.OcppServer.Models.Internal;
using PubSub.OcppServer.Models.Ocpp.v16;

namespace PubSub.OcppServer.Models.FramingProtocol
{
    public class TcsAndResponseType
    {
        public TaskCompletionSource<OcppResponseOrError> Tcs { get; set; }
        public Type OcppResponseType { get; set; }
    }
}
