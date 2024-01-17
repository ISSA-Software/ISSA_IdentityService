using Confluent.Kafka;
using ISSA_IdentityService.Core.Config;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ISSA_IdentityService.Service.BaseService
{
    public class KafkaMessageConsumer : BackgroundService
    {
        private readonly IConsumer<Ignore, string> _consumer;

        private readonly ILogger<KafkaMessageConsumer> _logger;

        public KafkaMessageConsumer( ILogger<KafkaMessageConsumer> logger)
        {
            _logger = logger;

            var consumerConfig = new ConsumerConfig
            {
                BootstrapServers = SystemSettingModel.Configs?["Kafka:BootstrapServers"],
                GroupId = "IdentityConsumerGroup",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            _consumer = new ConsumerBuilder<Ignore, string>(consumerConfig).Build();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _consumer.Subscribe("IdentityUpdate");
            while (!stoppingToken.IsCancellationRequested)
            {
                ProcessKafkaMessage(stoppingToken);
                await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
            }
            _consumer.Close();
        }

        public void ProcessKafkaMessage(CancellationToken stoppingToken)
        {
            try
            {
                var consumeResult = _consumer.Consume(stoppingToken);
                var message = consumeResult.Message.Value;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error processing Kafka message: {ex.Message}");
            }
        }
    }
}