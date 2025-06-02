namespace PubSub.OcppServer.Models.Ocpp.v201;

public class MessageContent
{
    public MessageFormatEnum Format { get; set; }
    public string? Language { get; set; }
    public string Content { get; set; }
}