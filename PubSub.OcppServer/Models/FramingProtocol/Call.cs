using System.Text.Json;

namespace PubSub.OcppServer.Models.FramingProtocol
{
    public class Call : CallBase
    {
        public Call() { }
        public int? MessageTypeId { get; set; }
        public string? Action { get; set; }
        public object? Payload { get; set; }
        public Call(string rawMessage)
        {
            var splitFrameMessage = JsonSerializer.Deserialize<object[]>(rawMessage);
            if (splitFrameMessage == null) return;
            UniqueId = splitFrameMessage[1].ToString();
            Action = splitFrameMessage[2].ToString();
            Payload = splitFrameMessage[3];
        }


      
    }
}
