using PubSub.OcppServer.Data.Interfaces;
using PubSub.OcppServer.Models.EF;
using System.Security.Claims;
using PubSub.OcppServer.Models.Dtos;
using PubSub.OcppServer.Services.Interfaces;

namespace PubSub.OcppServer.Services
{
    public class UserManagementService : IUserManagementService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly ISecurityProfileHandler _securityProfileHandler;

        public UserManagementService(
            IUnitOfWork unitOfWork,
            IJwtTokenService jwtTokenService,
            IEnumerable<ISecurityProfileHandler> securityProfileHandlers)
        {
            _unitOfWork = unitOfWork;
            _jwtTokenService = jwtTokenService;
            _securityProfileHandler = securityProfileHandlers.OfType<BasicAuthSecurityHandler>().FirstOrDefault();

        }

        public bool CreateUser(UserInfoWithPasswordDto userInfo)
        {
            var userExists = _unitOfWork
                .ApiUsers
                .Find(u => u.Email == userInfo.UserId);
            if (userExists.Any()) return false;
            var hashed = _securityProfileHandler
                .HashPassword(userInfo.Password, out string saltString);
            var user = new ApiUser
            {
                Email = userInfo.UserId,
                FirstName = userInfo.FirstName,
                LastName = userInfo.LastName,
                IdTags = new List<IdTag>(),
                UserId = userInfo.UserId,
                HashedPassword = hashed,
                Salt = saltString,
                IsAdmin = false
            };
            _unitOfWork.ApiUsers.Add(user);
           
            _unitOfWork.Complete();
            return true;
        }

        public bool VerifyUserEmailExists(UserInfoDto user)
        {
            if (user.UserId == null) return false;
            return _unitOfWork
                .ApiUsers
                .Find(a => a.Email == user.UserId)
                .Any();
        }
        public Tuple<string,UserInfoWithAccessTokenDto> GetUser(string user)
        {
            var storedUser = _unitOfWork.ApiUsers
                .Find(u => u.UserId == user)
                .FirstOrDefault();
            var refreshToken = FetchAndStoreUpdatedRefreshToken(storedUser);
            _unitOfWork.Complete();
            if (refreshToken == null) return null;
            return new Tuple<string, UserInfoWithAccessTokenDto>(refreshToken, 
          
                new UserInfoWithAccessTokenDto() 
                {
                    AccessToken = "",
                    ExpiredAt = DateTimeOffset.Now.AddMinutes(_jwtTokenService.Settings.RefreshTokenValidityMinutes).ToUnixTimeSeconds(),
                    FirstName = storedUser.FirstName,
                    LastName = storedUser.LastName,
                    UserId = storedUser.Email,
                    UserName = storedUser.Email
                });
        }

        public Tuple<string, UserInfoWithAccessTokenDto> GetUserWithNewRefreshTokenByRefreshToken(string oldRefreshToken)
        {
            var storedUser = _unitOfWork.ApiUsers
                .Find(r => r.RefreshToken == oldRefreshToken)
                .FirstOrDefault();
            if (storedUser == null || storedUser.TokenExpiry < DateTime.UtcNow)
            {
                return null;
            }
            var newRefreshToken = _jwtTokenService.GenerateRefreshToken();
            var refreshTokenExpiredAt = DateTimeOffset.UtcNow.AddMinutes(_jwtTokenService.Settings.RefreshTokenValidityMinutes);
            storedUser.RefreshToken = newRefreshToken;
            storedUser.TokenExpiry = refreshTokenExpiredAt;
            _unitOfWork.Complete();
            if (newRefreshToken == "") return null;
            return new Tuple<string, UserInfoWithAccessTokenDto>(newRefreshToken, new UserInfoWithAccessTokenDto()
            {
                AccessToken = "",
                ExpiredAt = DateTimeOffset.Now.AddMinutes(_jwtTokenService.Settings.RefreshTokenValidityMinutes).ToUnixTimeSeconds(),
                FirstName = storedUser.FirstName,
                LastName = storedUser.LastName,
                UserId = storedUser.Email,
                UserName = storedUser.Email

            });
        }

        private string FetchAndStoreUpdatedRefreshToken(ApiUser? storedUser)
        {
            if (storedUser == null)
            {
                return "";
            }

            var newRefreshToken = _jwtTokenService.GenerateRefreshToken();
            var refreshTokenExpiredAt = DateTimeOffset.UtcNow.AddMinutes(_jwtTokenService.Settings.RefreshTokenValidityMinutes);
            storedUser.RefreshToken = newRefreshToken;
            storedUser.TokenExpiry = refreshTokenExpiredAt;
            
            return newRefreshToken;
        }

        public void RemoveRefreshToken(string refreshToken)
        {
            var userWithTokenToRemove = _unitOfWork
                .ApiUsers
                .Find(r => r.RefreshToken == refreshToken)
                .FirstOrDefault();
            if (userWithTokenToRemove != null)
            {
                userWithTokenToRemove.RefreshToken = String.Empty;
            }

            _unitOfWork.Complete();
        }

        public void SendVerificationEmail(ApiUser apiUser)
        {
            
        }

        public bool VerifyEmailedToken(string verificationToken)
        {
            return false;
        }

        public string RegisterChargingPointUser(string username)
        {
            var credentialsWithSalt = _securityProfileHandler
                .GenerateRandomCredentialsWithSalt(username);
            try
            {
                var chargingPointUser = new ChargePointUser()
                {
                    ChargePointName = username,
                    HashedPassword = credentialsWithSalt.HashedPassword,
                    Salt = credentialsWithSalt.Salt,
                    UserId = username
                };
                _unitOfWork.ChargePointUsers.Add(chargingPointUser);
                _unitOfWork.Complete();
                return credentialsWithSalt.UnhashedPassword;
            }
            catch
            {
                return "";
            }
        }
    }
}
