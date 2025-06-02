using System.Text.Json;

namespace PubSub.OcppServer.Models.FramingProtocol
{
    public class CallError : CallBase
    {
        public int MessageTypeId { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorDescription { get; set; }
        public object? ErrorDetails { get; set; }
        public CallError(Call call, string errorCode, string errorDescription, object? errorDetails)
        {
            MessageTypeId = 4;
            UniqueId = call.UniqueId;
            ErrorCode = errorCode;
            ErrorDescription = errorDescription;
            ErrorDetails = errorDetails;
        }
        public CallError(string rawMessage)
        {
            var splitFrameMessage = JsonSerializer.Deserialize<object[]>(rawMessage);
            UniqueId = splitFrameMessage[1].ToString();
            ErrorCode = splitFrameMessage[2].ToString();
            ErrorDescription = splitFrameMessage[3].ToString();
            ErrorDetails = splitFrameMessage[4];
        }

    }
}
