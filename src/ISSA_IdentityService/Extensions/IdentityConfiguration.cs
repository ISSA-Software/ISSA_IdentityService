using ISSA_IdentityService.Contract.Repository.Entity.IdentityModels;
using ISSA_IdentityService.Core.Config;
using ISSA_IdentityService.Repository.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ISSA_IdentityService.Extensions
{
    public static class IdentityConfiguration
    {
        public static IServiceCollection ConfigureAuthentication(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
            }).AddRoleManager<RoleManager<IdentityRole>>()
              .AddRoles<IdentityRole>()
              .AddEntityFrameworkStores<AppDbContext>()
              .AddDefaultTokenProviders();
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
                    ValidAudience = SystemSettingModel.Configs["Jwt:ValidAudience"],
                    ValidIssuer = SystemSettingModel.Configs["Jwt:ValidIssuer"],
                    IssuerSigningKey = SystemSettingModel.RSAPublicKey ?? new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SystemSettingModel.Configs["Jwt:SecrectKey"])),
                };
            });

            return services;
        }
    }
}