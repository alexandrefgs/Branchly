namespace Branchly.Auth.Extensions;

public static class CorsConfig
{
    public static IServiceCollection AddCorsConfig(this IServiceCollection services, IConfiguration cfg)
    {
        var origins = cfg.GetSection("Cors:Origins").Get<string[]>() ?? Array.Empty<string>();

        services.AddCors(opt =>
        {
            opt.AddPolicy("default", p => p.WithOrigins(origins)
                                           .AllowAnyHeader()
                                           .AllowAnyMethod());
        });

        return services;
    }
}
