using Confluent.Kafka;
using ISSA_IdentityService.Contract.Service.Interface.InternalService;
using ISSA_IdentityService.Core.Config;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ISSA_IdentityService.Service.Services.InternalServices.Kafka
{
    public class KafkaConsumer : BackgroundService, IKafkaConsumer
    {
        private readonly IConsumer<Ignore, string> _consumer;
        private readonly ILogger<KafkaConsumer> _logger;

        public KafkaConsumer(ILogger<KafkaConsumer> logger)
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


        public async Task Consume(CancellationToken cancellationToken = default)
        {
            _consumer.Subscribe("IdentityUpdate");
            while (!cancellationToken.IsCancellationRequested)
            {
                ProcessKafkaMessage(cancellationToken);
                await Task.Delay(TimeSpan.FromSeconds(30), cancellationToken);
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

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Consume(stoppingToken);
        }
    }
}
