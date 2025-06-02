using PubSub.OcppServer.Models.EventArguments;

namespace PubSub.OcppServer.Services.Interfaces
{
    public interface ISendMessageBus
    {
        event EventHandler<OcppResponseArgs>? OnSendMessage;
        public void Add(string frameMessage);
        public bool TryFetch(out string frameMessage);
    }
}
