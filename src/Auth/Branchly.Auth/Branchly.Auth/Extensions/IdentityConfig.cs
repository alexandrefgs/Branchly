using Branchly.Auth.Data;
using Branchly.Auth.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Branchly.Auth.Extensions;

public static class IdentityConfig
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration cfg)
    {
        services.AddDbContext<AuthDbContext>(options =>
            options.UseSqlServer(cfg.GetConnectionString("Default")));
        return services;
    }

    public static IServiceCollection AddIdentityConfig(this IServiceCollection services)
    {
        services.AddIdentityCore<ApplicationUser>(options =>
        {
            options.User.RequireUniqueEmail = true;

            options.Password.RequiredLength = 8;
            options.Password.RequireDigit = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireNonAlphanumeric = false;

            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
        })
        .AddEntityFrameworkStores<AuthDbContext>()
        .AddSignInManager()
        .AddDefaultTokenProviders();

        return services;
    }
}
