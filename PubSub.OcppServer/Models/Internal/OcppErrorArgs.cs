namespace PubSub.OcppServer.Models.Internal;

public class OcppErrorArgs : EventArgs
{
    public Exception Exception { get; set; }
}