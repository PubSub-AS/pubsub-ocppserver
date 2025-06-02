using PubSub.OcppServer.Models.Dtos;
using PubSub.OcppServer.Models.EF;

namespace PubSub.OcppServer.Services.Interfaces;

public interface IUserManagementService
{
    bool CreateUser(UserInfoWithPasswordDto userInfo);
    bool VerifyUserEmailExists(UserInfoDto user);
    Tuple<string, UserInfoWithAccessTokenDto> GetUser(string user);
    Tuple<string, UserInfoWithAccessTokenDto> GetUserWithNewRefreshTokenByRefreshToken(string oldRefreshToken);
    void RemoveRefreshToken(string refreshToken);
    void SendVerificationEmail(ApiUser apiUser);
    bool VerifyEmailedToken(string verificationToken);
    string RegisterChargingPointUser(string username);
}