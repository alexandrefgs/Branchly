using Branchly.Auth.DTOs;
using Branchly.Auth.Models;
using Branchly.Auth.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.AspNetCore.WebUtilities;
using System.Text.Encodings.Web;

namespace Branchly.Auth.Controllers;

[ApiController]
[Route("v1/auth")]
public class AuthController(
    IAuthService auth,
    UserManager<ApplicationUser> users,
    IEmailSender email,
    IConfiguration config) : ControllerBase
{
    private string BuildUrl(string path, IDictionary<string, string?> qs)
    {
        var baseUrl = config["Frontend:BaseUrl"]
                      ?? $"{Request.Scheme}://{Request.Host}";
        var url = new Uri(new Uri(baseUrl.TrimEnd('/') + "/"), path.TrimStart('/')).ToString();
        return QueryHelpers.AddQueryString(url, qs!);
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest req)
    {
        var (result, resp) = await auth.RegisterAsync(req);
        if (!result.Succeeded)
        {
            var ms = new ModelStateDictionary();
            foreach (var e in result.Errors) ms.AddModelError(e.Code, e.Description);
            return ValidationProblem(modelStateDictionary: ms);
        }

        var userId = resp!.UserId;
        var user = await users.FindByIdAsync(userId);
        var token = await users.GenerateEmailConfirmationTokenAsync(user!);

        var verifyUrl = BuildUrl("/v1/auth/verify-email", new Dictionary<string, string?>
        {
            ["uid"] = userId,
            ["token"] = UrlEncoder.Default.Encode(token)
        });

        await email.SendAsync(user!.Email!, "Confirm your Branchly account",
            $"<p>Click to confirm: <a href=\"{verifyUrl}\">Verify email</a></p>");

        return Created($"/v1/auth/users/{userId}", new { userId, requiresEmailConfirmation = true });
    }

    [AllowAnonymous]
    [HttpGet("verify-email")]
    public async Task<IActionResult> VerifyEmail([FromQuery] VerifyEmailQuery q)
    {
        var user = await users.FindByIdAsync(q.UserId);
        if (user is null) return NotFound();

        var token = System.Net.WebUtility.UrlDecode(q.Token);
        var result = await users.ConfirmEmailAsync(user, token);
        return result.Succeeded ? Ok(new { message = "Email confirmed" })
                                : BadRequest(result.Errors);
    }

    [AllowAnonymous]
    [HttpPost("forgot")]
    public async Task<IActionResult> Forgot([FromBody] ForgotPasswordRequest body)
    {
        var user = await users.FindByEmailAsync(body.Email);
        if (user is null) return NoContent(); // não revela existência

        var token = await users.GeneratePasswordResetTokenAsync(user);
        var resetUrl = BuildUrl("/v1/auth/reset", new Dictionary<string, string?>
        {
            ["uid"] = user.Id,
            ["token"] = UrlEncoder.Default.Encode(token)
        });

        await email.SendAsync(user.Email!, "Reset your Branchly password",
            $"<p>Reset link: <a href=\"{resetUrl}\">Reset password</a></p>");

        return NoContent();
    }

    [AllowAnonymous]
    [HttpPost("reset")]
    public async Task<IActionResult> Reset([FromBody] ResetPasswordRequest body)
    {
        var user = await users.FindByIdAsync(body.UserId);
        if (user is null) return BadRequest();

        var token = System.Net.WebUtility.UrlDecode(body.Token);
        var result = await users.ResetPasswordAsync(user, token, body.NewPassword);
        return result.Succeeded ? NoContent() : BadRequest(result.Errors);
    }

    [AllowAnonymous]
    [EnableRateLimiting("AuthTight")]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest req)
    {
        var resp = await auth.LoginAsync(req);
        return resp is null ? Unauthorized() : Ok(resp);
    }

    [AllowAnonymous]
    [EnableRateLimiting("AuthTight")]
    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] RefreshRequest req)
    {
        var resp = await auth.RefreshAsync(req.RefreshToken);
        return resp is null ? Unauthorized() : Ok(resp);
    }

    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout([FromBody] LogoutRequest req)
    {
        var ok = await auth.LogoutAsync(req.RefreshToken);
        return ok ? NoContent() : BadRequest();
    }
}
