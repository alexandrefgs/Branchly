using System.Security.Claims;

namespace Branchly.Auth.Services;

public interface ITokenService
{
    (string token, DateTime expiresAt) CreateAccessToken(IEnumerable<Claim> claims);
    string CreateRefreshToken();     // string opaca; persistimos só o hash
    byte[] Hash(string value);
}
