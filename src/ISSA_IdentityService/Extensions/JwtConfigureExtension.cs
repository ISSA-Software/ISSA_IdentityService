using ISSA_IdentityService.Core.Config;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace ISSA_IdentityService.Extensions
{
    public static class JwtConfigureExtension
    {
        public static IServiceCollection ConfigureJwtAuth(this IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    LogValidationExceptions = true,
                    ValidAudience = SystemSettingModel.Configs?["Jwt:ValidAudience"],
                    ValidIssuer = SystemSettingModel.Configs?["Jwt:ValidIssuer"],
                    IssuerSigningKey = SystemSettingModel.RSAPublicKey,
                };
            });
            return services;
        }
    }
}
