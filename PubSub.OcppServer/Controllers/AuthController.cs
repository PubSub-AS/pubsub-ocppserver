using System.Security.Claims;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using NuGet.Protocol;
using PubSub.OcppServer.Data.Interfaces;
using PubSub.OcppServer.Models.Dtos;
using PubSub.OcppServer.Models.EF;
using PubSub.OcppServer.Models.Internal;
using PubSub.OcppServer.Services;
using PubSub.OcppServer.Services.Interfaces;

namespace PubSub.OcppServer.Controllers
{
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IUserManagementService _userManagementService;
        private readonly ILogger<AuthController> _logger;
        private readonly ISecurityProfileHandler _securityProfileHandler;
        

        public AuthController(IJwtTokenService jwtTokenService, 
            IUserManagementService userManagementService,
            IEnumerable<ISecurityProfileHandler> securityProfileHandlers,
            ILogger<AuthController> logger)
        {
            _jwtTokenService = jwtTokenService;
            _userManagementService = userManagementService;
            _securityProfileHandler = securityProfileHandlers.OfType<BasicAuthSecurityHandler>().FirstOrDefault() ?? throw new InvalidOperationException();
            _logger = logger;
        }

        [HttpPost("/ocpp/auth/login")]
        public IActionResult Login([FromBody] UsernameAndPasswordDto request)
        {
            var req = HttpContext.Request;
            var verifiedCredentials =
                (ApiUser)_securityProfileHandler.VerifyCredentials(request.Username, request.Password);
            if (verifiedCredentials == null) return Unauthorized("Wrong username or password");
            
            var claims = new List<Claim>()
            {
                new(ClaimTypes.Email, request.Username),
                new ("IsAdmin", verifiedCredentials.IsAdmin.ToString())
            };
            var accessToken = _jwtTokenService.GenerateAccessToken(claims);

            var user = _userManagementService.GetUser(request.Username);
            if (user == null) return Unauthorized("Unknown user");

            return CreateTokenResponse(accessToken, user.Item1, user.Item2);
        }
        [HttpGet("/ocpp/auth/refresh/{oldRefreshToken}")]
        public IActionResult Refresh(string oldRefreshToken)
        {
            //var oldRefreshToken = Request.Cookies["refreshToken"];
           
            if (string.IsNullOrEmpty(oldRefreshToken))
            {
                return Ok(new UserInfoWithAccessTokenDto());
            }
            var storedUserTuple = _userManagementService.GetUserWithNewRefreshTokenByRefreshToken(oldRefreshToken);
            if (storedUserTuple == null)
            {
                return Unauthorized("Unknown credentials. Please log off and on.");
            }
            
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, storedUserTuple.Item2.UserId)
            };
            var newAccessToken = _jwtTokenService.GenerateAccessToken(claims);
          
            return CreateTokenResponse(
                newAccessToken,
                storedUserTuple.Item1,
                storedUserTuple.Item2);

        }
        private IActionResult CreateTokenResponse(string accessToken, string refreshToken, UserInfoWithAccessTokenDto user)
        {
            // Refresh token as cookie
            /*
            _logger.LogDebug("Setting cookie with refreshtoken: " + refreshToken);
            Response.Cookies.Append("refreshToken", refreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                Path = "/",
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddMinutes(_jwtTokenService.Settings.RefreshTokenValidityMinutes),
                Domain = Request.IsHttps ? ".pubsub.no" : "localhost",
                IsEssential = true
            });
            */
           
            return Ok(new UserInfoWithAccessTokenDto()
            {
                UserId = user.UserId,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                ExpiredAt = DateTimeOffset.UtcNow.AddMinutes(_jwtTokenService.Settings.AccessTokenValidityMinutes).ToUnixTimeSeconds()
            });
        }
        
        [HttpPost("/ocpp/auth/logout")]
        public IActionResult Logout([FromBody] string refreshToken)
        {
            //var refreshToken = Request.Cookies["refreshToken"];
            if (!string.IsNullOrEmpty(refreshToken))
            {
                _userManagementService.RemoveRefreshToken(refreshToken);
                Response.Cookies.Delete("refreshToken");
            }
            return NoContent();
        }
        
        [HttpGet("/ocpp/auth/hashpassword/{password}")]
        public Tuple<string, string> HashPassword(string password)
        {
            var hashed = _securityProfileHandler.HashPassword(password, out string saltString);
            return Tuple.Create(hashed, saltString);
        }
        [HttpPost("/ocpp/auth/register")]
        public async Task<IActionResult> Register([FromBody] UserInfoWithPasswordDto userinfo)
        {
         
            if (!_userManagementService.VerifyUserEmailExists(userinfo)) return BadRequest();
            var result = _userManagementService.CreateUser(userinfo);

            if (result)
            {
                // Optionally send confirmation email here

                return Ok(new { message = "User created successfully" });
            }

            return BadRequest();
        }
        [HttpGet("/ocpp/auth/registerchargingpointuser/{username}")]

        public async Task<IActionResult> RegisterChargingPointUser(string username)
        {
            var password = _userManagementService.RegisterChargingPointUser(username);
            if (password == "")
            {
                return BadRequest("ChargingPoint user can't be registered.");}

            return Ok(password);
        }
    }

}

