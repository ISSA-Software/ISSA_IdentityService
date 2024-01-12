using Invedia.DI;
using ISSA_IdentityService.Core.Config;
using ISSA_IdentityService.Repository.Infrastructure;
using ISSA_IdentityService.Service.BaseService;
using ISSA_IdentityService.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Serilog;
using StackExchange.Redis;
using System.IO.Compression;
using System.Text.RegularExpressions;

namespace ISSA_IdentityService
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            SystemSettingModel.Environment = builder.Environment.EnvironmentName;

            SystemSettingModel.Configs = builder.Configuration.AddJsonFile("appsettings.json", false, true)
               .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", false, true)
               .AddUserSecrets<Program>(true, false)
               .Build();


            // Add services to the container.
            builder.Host.UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration).Enrich.FromLogContext().WriteTo.Console());

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

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddHealthChecks();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "ISSA", Version = "v1" });
                var jwtSecurityScheme = new OpenApiSecurityScheme
                {
                    BearerFormat = "JWT",
                    Name = "JWT Authentication",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                    Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };

                options.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { jwtSecurityScheme, Array.Empty<string>() }
                });
            });

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

            _ = builder.Services.AddSingleton<IConnectionMultiplexer>(await ConnectionMultiplexer.ConnectAsync(configuration: builder.Configuration.GetConnectionString("RedisConnection") ?? string.Empty));

            builder.Services.ConfigureAuthentication();
            builder.Services.AddAutoMapperServices();

            builder.Services.AddRouting(options =>
            {
                options.AppendTrailingSlash = false;
            });

            _ = builder.Services.AddSystemSetting(builder.Configuration.GetSection("SystemSetting").Get<SystemSettingModel>());
            builder.Services.Configure<DataProtectionTokenProviderOptions>(opt => opt.TokenLifespan = TimeSpan.FromMinutes(30));
            builder.Services.AddDI();
            builder.Services.PrintServiceAddedToConsole();
            builder.Services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Optimal;
            });
            builder.Services.Configure<BrotliCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Optimal;
            });
            builder.Services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
                options.Providers.Add<GzipCompressionProvider>();
                options.Providers.Add<BrotliCompressionProvider>();
            });

            builder.Services.AddHostedService<BackgroundTaskConsumer>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseSwagger();
            app.UseSwaggerUI(
                options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "ISSA_IdentityService v1");
                    options.RoutePrefix = "swagger/api/v1";
                });
            IdentityModelEventSource.ShowPII = true;
            //}
            //app.UseCors(builder.Environment.EnvironmentName);
            app.UseCors("Default");
            app.UseHttpsRedirection();
            app.UseSerilogRequestLogging();
            app.UseAuthentication();
            app.MapControllers();
            app.UseRouting();
            //app.UseIdentityServer();
            app.UseAuthorization();
            app.UseResponseCompression();
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
