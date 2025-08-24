using Branchly.Auth.DTOs;
using Branchly.Auth.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Branchly.Auth.Controllers;

[ApiController]
[Route("v1/auth")]
public class AuthController(IAuthService auth) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest req)
    {
        var (result, resp) = await auth.RegisterAsync(req);
        if (!result.Succeeded)
        {
            var modelState = new ModelStateDictionary();
            foreach (var err in result.Errors)
                modelState.AddModelError(err.Code, err.Description);

            return ValidationProblem(modelStateDictionary: modelState);
        }
        return Created($"/v1/auth/users/{resp!.UserId}", resp);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest req)
    {
        var resp = await auth.LoginAsync(req);
        return resp is null ? Unauthorized() : Ok(resp);
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] RefreshRequest req)
    {
        var resp = await auth.RefreshAsync(req.RefreshToken);
        return resp is null ? Unauthorized() : Ok(resp);
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout([FromBody] LogoutRequest req)
    {
        var ok = await auth.LogoutAsync(req.RefreshToken);
        return ok ? NoContent() : BadRequest();
    }
}
