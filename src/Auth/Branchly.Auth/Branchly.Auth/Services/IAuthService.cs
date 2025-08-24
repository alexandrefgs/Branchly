using Microsoft.AspNetCore.Identity;
using Branchly.Auth.DTOs;

namespace Branchly.Auth.Services;

public interface IAuthService
{
    Task<(IdentityResult result, RegisterResponse? response)> RegisterAsync(RegisterRequest req);
    Task<AuthResponse?> LoginAsync(LoginRequest req);
    Task<AuthResponse?> RefreshAsync(string refreshToken);
    Task<bool> LogoutAsync(string refreshToken);
}
