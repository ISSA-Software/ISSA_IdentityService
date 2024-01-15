using Microsoft.AspNetCore.ResponseCompression;
using System.IO.Compression;

namespace ISSA_IdentityService.Extensions
{
    public static class ResponseCompressionExtenstion
    {
        public static IServiceCollection ConfigureResponseCompression(this IServiceCollection services)
        {
            services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Optimal;
            });
            services.Configure<BrotliCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Optimal;
            });
            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
                options.Providers.Add<GzipCompressionProvider>();
                options.Providers.Add<BrotliCompressionProvider>();
            });
            return services;
        }
    }
}
