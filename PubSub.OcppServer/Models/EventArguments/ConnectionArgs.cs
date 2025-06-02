using PubSub.OcppServer.Services;

namespace PubSub.OcppServer.Models.EventArguments;

public class ConnectionArgs : System.EventArgs
{
    public ConnectionStatusEnum ConnectionStatus { get; set; }
}