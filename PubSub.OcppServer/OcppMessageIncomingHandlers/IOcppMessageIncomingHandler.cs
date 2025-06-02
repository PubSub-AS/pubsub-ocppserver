

using PubSub.OcppServer.Models.Ocpp;

namespace PubSub.OcppServer.OcppMessageIncomingHandlers
{
    public interface IOcppMessageIncomingHandler<TRequest, TResponse>
        where TRequest : IOcppRequest
    {
        TResponse Handle(TRequest request);
    }
}
