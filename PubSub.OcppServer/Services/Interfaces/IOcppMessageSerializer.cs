using PubSub.OcppServer.Models.FramingProtocol;

namespace PubSub.OcppServer.Services.Interfaces
{
    public interface IOcppMessageSerializer
    {
        public string SerializeCall(Call call);

        public string SerializeCallResult(
            string uniqueId,
            object responsePayload
        );
        public string SerializeCallError(
            string uniqueId,
            string errorCode,
            string errorDescription
        );

        public object? DeserializeRequest(
            string? payload,
            Type? requestType);
    }
}
