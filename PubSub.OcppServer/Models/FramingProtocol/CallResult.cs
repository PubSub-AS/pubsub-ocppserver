using System.Text.Json;

namespace PubSub.OcppServer.Models.FramingProtocol
{
    public class CallResult : CallBase
    {

        public int MessageTypeId { get; set; }
        public object Payload { get; set; }
        public CallResult() { }
        public CallResult(string rawMessage)
        {
            var splitFrameMessage = JsonSerializer.Deserialize<object[]>(rawMessage);
            UniqueId = splitFrameMessage[1].ToString();
            Payload = splitFrameMessage[2];
        }

    }
}
