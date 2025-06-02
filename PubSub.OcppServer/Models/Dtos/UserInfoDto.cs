using System.Security;

namespace PubSub.OcppServer.Models.Dtos;

public abstract class UserInfoDto
{
    public string? UserId { get; set; }
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public long ExpiredAt { get; set; }
}

public class UserInfoWithPasswordDto : UserInfoDto
{
    public string Password { get; set; }
}

public class UserInfoWithAccessTokenDto : UserInfoDto
{
    public string AccessToken { get; set; }
    public string? RefreshToken { get; set; }
}