using System.Security.Claims;
using Branchly.Auth.Data;
using Branchly.Auth.DTOs;
using Branchly.Auth.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Branchly.Auth.Services;

public class AuthService(
    UserManager<ApplicationUser> users,
    SignInManager<ApplicationUser> signIn,
    AuthDbContext db,
    ITokenService tokens) : IAuthService
{
    public async Task<(IdentityResult result, RegisterResponse? response)> RegisterAsync(RegisterRequest req)
    {
        var user = new ApplicationUser { UserName = req.Email, Email = req.Email, EmailConfirmed = false };
        var result = await users.CreateAsync(user, req.Password);
        if (!result.Succeeded) return (result, null);
        return (result, new RegisterResponse(user.Id, true));
    }

    public async Task<AuthResponse?> LoginAsync(LoginRequest req)
    {
        var user = await users.FindByEmailAsync(req.Email);
        if (user is null) return null;

        var ok = await signIn.CheckPasswordSignInAsync(user, req.Password, lockoutOnFailure: true);
        if (!ok.Succeeded) return null;

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id),
            new("scope","users.read links.read links.write")
        };
        var (access, exp) = tokens.CreateAccessToken(claims);
        var refresh = tokens.CreateRefreshToken();

        db.RefreshTokens.Add(new RefreshToken
        {
            UserId = user.Id,
            TokenHash = tokens.Hash(refresh),
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = DateTime.UtcNow.AddDays(7)
        });
        await db.SaveChangesAsync();

        return new AuthResponse(access, (int)(exp - DateTime.UtcNow).TotalSeconds, refresh, "users.read links.read links.write");
    }

    public async Task<AuthResponse?> RefreshAsync(string refreshToken)
    {
        var hash = tokens.Hash(refreshToken);
        var rt = await db.RefreshTokens.FirstOrDefaultAsync(x => x.TokenHash == hash);
        if (rt is null || rt.RevokedAt != null || rt.ExpiresAt < DateTime.UtcNow) return null;

        rt.RevokedAt = DateTime.UtcNow;

        var user = await db.Users.FirstAsync(u => u.Id == rt.UserId);
        var claims = new List<Claim> {
            new(ClaimTypes.NameIdentifier, user.Id),
            new("scope","users.read links.read links.write")
        };
        var (access, exp) = tokens.CreateAccessToken(claims);
        var newRefresh = tokens.CreateRefreshToken();

        var newEntity = new RefreshToken
        {
            UserId = user.Id,
            TokenHash = tokens.Hash(newRefresh),
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = DateTime.UtcNow.AddDays(7)
        };
        rt.ReplacedByTokenHash = newEntity.TokenHash;
        db.RefreshTokens.Add(newEntity);
        await db.SaveChangesAsync();

        return new AuthResponse(access, (int)(exp - DateTime.UtcNow).TotalSeconds, newRefresh, "users.read links.read links.write");
    }

    public async Task<bool> LogoutAsync(string refreshToken)
    {
        var hash = tokens.Hash(refreshToken);
        var rt = await db.RefreshTokens.FirstOrDefaultAsync(x => x.TokenHash == hash && x.RevokedAt == null);
        if (rt is null) return false;
        rt.RevokedAt = DateTime.UtcNow;
        await db.SaveChangesAsync();
        return true;
    }
}