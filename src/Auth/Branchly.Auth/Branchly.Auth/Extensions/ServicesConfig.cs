using Branchly.Auth.Services;

namespace Branchly.Auth.Extensions;

public static class ServicesConfig
{
    public static IServiceCollection AddAppServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IEmailSender, EmailSender>();
        return services;
    }
}
