using ISSA_IdentityService.Contract.Repository.Entity.IdentityModels;
using ISSA_IdentityService.Core.Config;
using ISSA_IdentityService.Repository.Infrastructure;
using Microsoft.AspNetCore.Identity;

namespace ISSA_IdentityService.Extensions
{
    public static class IdentityConfiguration
    {
        public static IServiceCollection ConfigureIdentity(this IServiceCollection services)
        {
            if (SystemSettingModel.Configs == null)
            {
                throw new Exception("Jwt:ValidIssuer is null");
            }
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
            return services;
        }
    }
}