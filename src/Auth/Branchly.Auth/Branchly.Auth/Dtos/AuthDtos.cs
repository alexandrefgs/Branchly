namespace Branchly.Auth.DTOs;

public record RegisterRequest(string Email, string Password, string? DisplayName);
public record RegisterResponse(string UserId, bool RequiresEmailConfirmation);
public record LoginRequest(string Email, string Password);
public record AuthResponse(string AccessToken, int ExpiresIn, string RefreshToken, string Scope);
public record RefreshRequest(string RefreshToken);
public record LogoutRequest(string RefreshToken);
public record VerifyEmailQuery(string UserId, string Token);
public record ForgotPasswordRequest(string Email);
public record ResetPasswordRequest(string UserId, string Token, string NewPassword);
public record ResendVerificationRequest(string Email);