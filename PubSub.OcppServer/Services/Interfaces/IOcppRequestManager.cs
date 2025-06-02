using PubSub.OcppServer.Models.FramingProtocol;
using PubSub.OcppServer.Models.Internal;

namespace PubSub.OcppServer.Services.Interfaces;

public interface IOcppRequestManager
{
    Task<OcppResponseOrError> CreateAndSendPendingRequest(Call call, string requestFrameMessage, Type responseType);
    bool TryHandleResponse(CallResult callResult);
    bool TryHandleError(CallError callError);


}