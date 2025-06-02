using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PubSub.OcppServer.Data.Interfaces;
using PubSub.OcppServer.Models.Dtos;
using PubSub.OcppServer.Models.EF;
using PubSub.OcppServer.Services.Interfaces;

namespace PubSub.OcppServer.Controllers
{
    [Authorize]
    [ApiController]
    public class ChargingTransactionController : Controller
    {
        private readonly ILogger<ChargingTransactionController> _logger;
        private readonly IApiHandler _apiHandler;
        public ChargingTransactionController(ILogger<ChargingTransactionController> logger,
            IApiHandler apiHandler)
        {
            _logger = logger;
            _apiHandler = apiHandler;
        }

      
        [HttpGet("/ocpp/transactions")]
        public IEnumerable<TransactionDto> GetTransactions(bool onlyactive)
        {
            if (onlyactive)
                return _apiHandler.GetActiveTransactions();
            return _apiHandler.GetAllTransactions();
        }
     
        [HttpGet("/ocpp/transactions/transaction/{chargingTransactionId}")]
        public ActionResult<TransactionDto> GetTransaction(string chargingTransactionId)
        {
            var transaction = _apiHandler.GetTransactionById(chargingTransactionId);
            if (transaction == null) return NotFound($"There is no recent transaction with ID: {chargingTransactionId}");
            return Ok(transaction);
        }





    }
}