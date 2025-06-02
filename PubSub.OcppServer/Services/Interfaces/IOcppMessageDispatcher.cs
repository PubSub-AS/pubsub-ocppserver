using PubSub.OcppServer.Models.FramingProtocol;

namespace PubSub.OcppServer.Services.Interfaces;

public interface IOcppMessageDispatcher
{
    Task HandleIncomingOcppMessage(string incomingOcppMessage, string chargePointId);

}