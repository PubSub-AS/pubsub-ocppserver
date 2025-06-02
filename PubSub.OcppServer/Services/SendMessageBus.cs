using System.Collections.Concurrent;
using PubSub.OcppServer.Models.EventArguments;
using PubSub.OcppServer.Services.Interfaces;

namespace PubSub.OcppServer.Services
{
    public class SendMessageBus : ISendMessageBus
    {
        private readonly ConcurrentQueue<string> _messages = new();
        public event EventHandler<OcppResponseArgs>? OnSendMessage;

        public void Add(string frameMessage)
        {
            OnSendMessage?.Invoke(this, new OcppResponseArgs()
            {
                OcppResponseMessage = frameMessage
            });
            //_messages.Enqueue(frameMessage);
        }

        public bool TryFetch(out string frameMessage)
        {
            return _messages.TryDequeue(out frameMessage);
        }
    }
}
