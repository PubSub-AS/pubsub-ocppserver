using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PubSub.OcppServer.Services.Interfaces;

namespace PubSub.OcppServer.Controllers
{
    [Authorize]
    [ApiController]
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IApiHandler _apiHandler;
        public UserController(ILogger<UserController> logger,
            IApiHandler apiHandler)
        {
            _logger = logger;
            _apiHandler = apiHandler;
        }

        [HttpGet("/ocpp/user/{userId}")]
        public IActionResult GetUserName(string userId)
        {
            return Ok(_apiHandler.GetUserById(userId));
        }
    }
}
