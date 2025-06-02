using PubSub.OcppServer.Models.Dtos;
using PubSub.OcppServer.Models.Ocpp.v16;

namespace PubSub.OcppServer.OcppMessageIncomingHandlers.v16
{
    public class ClearCacheIncomingHandler : IOcppMessageIncomingHandler<ClearCacheRequest, ClearCacheResponse>
    {
        public ClearCacheResponse Handle(ClearCacheRequest request)
        {
            throw new NotImplementedException();
        }
     
    }
}
