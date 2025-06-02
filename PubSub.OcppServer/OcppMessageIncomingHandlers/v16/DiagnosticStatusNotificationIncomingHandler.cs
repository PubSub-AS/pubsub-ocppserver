using PubSub.OcppServer.Models.Ocpp.v16;

namespace PubSub.OcppServer.OcppMessageIncomingHandlers.v16
{
    public class DiagnosticStatusNotificationIncomingHandler : IOcppMessageIncomingHandler<DiagnosticStatusNotificationRequest, DiagnosticStatusNotificationResponse>
    {
        private readonly ILogger<DiagnosticStatusNotificationIncomingHandler> _logger;

        public DiagnosticStatusNotificationIncomingHandler(ILogger<DiagnosticStatusNotificationIncomingHandler> logger)
        {
            _logger = logger;
        }
        public DiagnosticStatusNotificationResponse Handle(DiagnosticStatusNotificationRequest request)
        {
            DiagnosticStatusNotificationResponse response = new();
            // implement support for notification receipt
            _logger.LogInformation("Received DiagnosticStatusNotificationRequest. Doin' nothing about it.");
            _logger.LogInformation("Status: " + request.Status.ToString());
            // TODO: Do something about it

            return response;
        }
    }
}
