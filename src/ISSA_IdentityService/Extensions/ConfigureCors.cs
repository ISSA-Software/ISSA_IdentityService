using ISSA_IdentityService.Core.Config;

namespace ISSA_IdentityService.Extensions
{
    public static class ConfigureCorsEX
    {
        public static IServiceCollection ConfigureCors(this IServiceCollection services)
        {
            if (SystemSettingModel.Configs == null)
            {
                throw new Exception("Jwt:ValidIssuer is null");
            }
            services.AddCors(options =>
            {
                options.AddPolicy("Development", policy =>
                {
                    policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
                options.AddPolicy("Production", policy =>
                {
                    policy.WithOrigins(SystemSettingModel.Configs["AllowedHosts"] ?? string.Empty)
                    .WithMethods(HttpMethod.Get.Method,
                    HttpMethod.Post.Method,
                    HttpMethod.Put.Method,
                    HttpMethod.Delete.Method,
                    HttpMethod.Patch.Method
                    ).AllowAnyHeader();
                });
                options.AddPolicy("Default", policy =>
                {
                    policy.AllowAnyOrigin()
                    .WithMethods(HttpMethod.Get.Method,
                    HttpMethod.Post.Method,
                    HttpMethod.Put.Method,
                    HttpMethod.Delete.Method,
                    HttpMethod.Patch.Method
                    ).AllowAnyHeader();
                });
            });
            return services;
        }
    }
}
