using PubSub.OcppServer.Models.FramingProtocol;
using PubSub.OcppServer.Models.Internal;
using PubSub.OcppServer.Services.Interfaces;
using System.Collections.Concurrent;
using System.Text.Json;

namespace PubSub.OcppServer.Services
{
    public class OcppRequestManager : IOcppRequestManager
    {
        private readonly ConcurrentDictionary<string, TcsAndResponseType> _pendingRequests = new();
        private readonly ISendMessageBus _ocppClientSendMessageBus;

        public OcppRequestManager(ISendMessageBus ocppClientSendMessageBus)
        {
            _ocppClientSendMessageBus = ocppClientSendMessageBus;
        }

        public Task<OcppResponseOrError> CreateAndSendPendingRequest(Call call, string requestFrameMessage, Type responseType)
        {
            var taskCompletionSource = new TaskCompletionSource<OcppResponseOrError>();
            _pendingRequests[call.UniqueId] = new TcsAndResponseType { OcppResponseType = responseType, Tcs = taskCompletionSource };
            _ocppClientSendMessageBus.Add(requestFrameMessage);
            return taskCompletionSource.Task;
        }

        public bool TryHandleResponse(CallResult callResult)
        {
            if (_pendingRequests.TryRemove(callResult.UniqueId, out var tcsAndType))
            {
                var responsePayload = JsonSerializer.Deserialize(callResult.Payload.ToString(), tcsAndType.OcppResponseType);
                var response = new OcppResponseOrError { OcppResponse = responsePayload };
                tcsAndType.Tcs.SetResult(response);
                return true;
            }
            return false;
        }

        public bool TryHandleError(CallError callError)
        {
            if (_pendingRequests.TryRemove(callError.UniqueId, out var tcsAndType))
            {
                var response = new OcppResponseOrError { ErrorCode = callError.ErrorCode, ErrorMessage = callError.ErrorDescription };
                tcsAndType.Tcs.SetResult(response);
                return true;
            }
            return false;
        }
    }
}
