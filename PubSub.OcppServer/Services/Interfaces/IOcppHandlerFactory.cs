using PubSub.OcppServer.Models.Internal;

namespace PubSub.OcppServer.Services.Interfaces
{
    public interface IOcppHandlerFactory
    {
        IOcppHandler CreateHandler(OcppVersionEnum ocppVersion);
    }
}
