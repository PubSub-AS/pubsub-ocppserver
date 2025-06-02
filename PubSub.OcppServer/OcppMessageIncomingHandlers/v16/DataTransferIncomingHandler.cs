using PubSub.OcppServer.Models.Ocpp.v16;
using PubSub.OcppServer.Services.Interfaces;
using PubSub.OcppServer.Services;

namespace PubSub.OcppServer.OcppMessageIncomingHandlers.v16
{
    public class DataTransferIncomingHandler : IOcppMessageIncomingHandler<DataTransferRequest, DataTransferResponse>
    {
        private readonly IOcppServer _ocppServer;
        private readonly ILogger<DataTransferIncomingHandler> _logger;
        private readonly OcppHandlerContext _context;

        public DataTransferIncomingHandler(IOcppServer ocppServer, ILogger<DataTransferIncomingHandler> logger,
            OcppHandlerContext context)
        {
            _ocppServer = ocppServer;
            _logger = logger;
            _context = context;
        }

        public DataTransferResponse Handle(DataTransferRequest request)
        {
            DataTransferResponse response = new();
            // TODO: test it

            if (_ocppServer.IsVendorKnown(request.VendorId))
            {
                response.Status = DataTransferStatus.Accepted;
                response.Data = "We have not yet implemented any handling of this request.";
                return response;
            }
            response.Status = DataTransferStatus.UnknownVendorId;
            response.Data = "";
            return response;
        }
    }
}
