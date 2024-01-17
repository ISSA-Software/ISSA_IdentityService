using Invedia.DI;
using ISSA_IdentityService.Core.Config;
using ISSA_IdentityService.Repository.Infrastructure;
using ISSA_IdentityService.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;
using Newtonsoft.Json;
using Serilog;
using StackExchange.Redis;
using System.Text.RegularExpressions;
using Grpc.AspNetCore.Server;
using Grpc.Net.Compression;
using ISSA_IdentityService.Service.Services.InternalServices;

namespace ISSA_IdentityService
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            ConfigureLogginExtension.ConfigureLogging();

            var builder = WebApplication.CreateBuilder(args);

            SystemSettingModel.Environment = builder.Environment.EnvironmentName;

            SystemSettingModel.Configs = builder.Configuration.AddJsonFile("appsettings.json", false, true)
               .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", false, true)
               .AddUserSecrets<Program>(true, false)
               .Build();
            builder.Host.UseSerilog();

            InitRSAKey.Init();

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.ConfigureCors();

            //builder.Services.AddValidatorsFromAssemblyContaining<>();

            //builder.Services.AddFluentValidationAutoValidation(options =>
            //{

            //});
           
            builder.Services.AddControllers(options =>
            {
                options.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
            }).AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            builder.Services.AddGrpc().AddServiceOptions<GrpcServiceOptions>(options =>
            {
                options.MaxReceiveMessageSize = 10 * 1024 * 1024;
                options.MaxSendMessageSize = 10 * 1024 * 1024;
                options.EnableDetailedErrors = true;
                options.ResponseCompressionLevel = System.IO.Compression.CompressionLevel.Optimal;
                options.CompressionProviders.Add(new GzipCompressionProvider(System.IO.Compression.CompressionLevel.Optimal));
                options.ResponseCompressionAlgorithm = "gzip";
            }).AddJsonTranscoding();
            builder.Services.AddGrpcReflection();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddHealthChecks();
            builder.Services.ConfigureSwagger();

            //builder.Services.AddSingleton(FirebaseApp.Create(new AppOptions()
            //{
            //    Credential = await GoogleCredential.FromFileAsync(builder.Configuration["Firebase:Credential"], cancellationToken: default),
            //    ServiceAccountId = builder.Configuration["Firebase:ServiceAccountId"],
            //}));

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(connectionString, x => x.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName));
            });

            builder.Services.AddMemoryCache();
            builder.Services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = builder.Configuration.GetConnectionString("RedisConnection");
            });

            builder.Services.AddSingleton<IConnectionMultiplexer>(await ConnectionMultiplexer.ConnectAsync(configuration: builder.Configuration.GetConnectionString("RedisConnection") ?? string.Empty));

            builder.Services.ConfigureJwtAuth();
            builder.Services.ConfigureIdentity();
            builder.Services.AddAutoMapperServices();
            builder.Services.AddRouting(options =>
            {
                options.AppendTrailingSlash = false;
            });
            builder.Services.AddSystemSetting(builder.Configuration.GetSection("SystemSetting").Get<SystemSettingModel>());
            builder.Services.Configure<DataProtectionTokenProviderOptions>(opt => opt.TokenLifespan = TimeSpan.FromMinutes(30));
            builder.Services.AddDI();
            builder.Services.PrintServiceAddedToConsole();
            builder.Services.ConfigureResponseCompression();
            builder.Services.AddHostedService<BackgroundTaskConsumer>();
            builder.Services.AddResponseCaching();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseSwagger();
            app.UseSwaggerUI(
                options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "ISSA IdentityService v1");
                    options.RoutePrefix = "swagger/api/v1";
                });
            IdentityModelEventSource.ShowPII = true;
            //}
            //app.UseCors(builder.Environment.EnvironmentName);
            app.UseCors("Default");
            app.UseHttpsRedirection();
            app.UseSerilogRequestLogging();
            app.MapGrpcService<Services.AdminRPCService>();
            app.MapGrpcService<Services.MentorRPCService>();
            app.MapGrpcService<Services.StudentRPCService>();
            app.MapControllers();
            app.UseRouting();
            //app.UseIdentityServer();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseResponseCompression();
            app.MapGrpcReflectionService();
            app.UseHealthChecks("/health");
            app.UseResponseCaching();
            app.Run();
        }
    }

    public class SlugifyParameterTransformer : IOutboundParameterTransformer
    {
        public string TransformOutbound(object? value)
        {
            var name = value?.ToString();
            return name == null ? string.Empty : Regex.Replace(name, "([a-z])([A-Z])", "$1-$2").ToLower();
        }
    }

    public static class StartupSystemSetting
    {
        public static IServiceCollection AddSystemSetting(this IServiceCollection services, SystemSettingModel? systemSettingModel)
        {
            SystemSettingModel.Instance = systemSettingModel ?? new SystemSettingModel();

            return services;
        }
    }
}
