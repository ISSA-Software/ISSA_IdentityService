using Serilog.Sinks.Elasticsearch;
using Serilog;
using System.Reflection;

namespace ISSA_IdentityService.Extensions
{
    public class ConfigureLogginExtension
    {
        public static void ConfigureLogging()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile(
                    $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json",
                    optional: true)
                .Build();

           Log.Logger = new LoggerConfiguration()
          .Enrich.FromLogContext()
          .Enrich.WithMachineName()
          .WriteTo.Debug()
          .WriteTo.Console()
          .WriteTo.Elasticsearch(ConfigureElasticSink(configuration, environment))
          .Enrich.WithProperty("Environment", environment)
          .ReadFrom.Configuration(configuration)
          .CreateLogger();
        }
        private static ElasticsearchSinkOptions ConfigureElasticSink(IConfigurationRoot configuration, string environment)
        {
            return new ElasticsearchSinkOptions(new Uri(configuration["ElasticConfiguration:Uri"] ?? string.Empty))
            {
                AutoRegisterTemplate = true,
                IndexFormat = $"{Assembly.GetExecutingAssembly()?.GetName()?.Name?.ToLower().Replace(".", "-")}-{environment?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}"
            };
        }
    }
}
