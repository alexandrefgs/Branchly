using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Branchly.Auth.Extensions;

public static class JwtConfig
{
    public static IServiceCollection AddJwtConfig(this IServiceCollection services, IConfiguration cfg)
    {
        var jwtKey = cfg["Jwt:SigningKey"] ?? throw new Exception("Jwt:SigningKey missing");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = cfg["Jwt:Issuer"],
                    ValidAudience = cfg["Jwt:Audience"],
                    IssuerSigningKey = key
                };
            });

        services.AddAuthorization();
        return services;
    }
}
