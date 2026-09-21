using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace RoomService.Application.Auth;

public static class AuthExtension
{
    public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
    {
        var securityKey = configuration.GetSection("AuthSettings").GetValue<string>("SecurityKey");
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(policy =>
        {
            policy.RequireHttpsMetadata = false;
            policy.SaveToken = true;

            policy.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey))
            };

            policy.Events = new JwtBearerEvents()
            {
                OnMessageReceived = context =>
                {
                    context.Token = context.Request.Cookies["tasty=cookie"];
                    return Task.CompletedTask;
                }
            };
        });

        services.AddAuthorization();
        return services;
    }
}
