using System.Text;
using Branchly.Auth.Data;
using Branchly.Auth.Models;
using Branchly.Auth.Services;
using Branchly.Auth.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AuthDbContext>(o =>
    o.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

var identity = builder.Services.AddIdentityCore<ApplicationUser>(o =>
{
    o.User.RequireUniqueEmail = true;
    o.Password.RequiredLength = 8;
    o.Password.RequireDigit = true;
    o.Password.RequireUppercase = true;
    o.Password.RequireNonAlphanumeric = false;
});
identity.AddEntityFrameworkStores<AuthDbContext>();
identity.AddSignInManager();

var signingKey = builder.Configuration["Jwt:SigningKey"] ?? throw new("Jwt:SigningKey missing");
var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(o =>
    {
        o.TokenValidationParameters = new()
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = key,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            ClockSkew = TimeSpan.FromSeconds(30)
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerConfiguration();

var app = builder.Build();

app.UseSwaggerConfiguration();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();

public partial class Program { }