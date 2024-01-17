using Confluent.Kafka;
using Invedia.DI.Attributes;
using ISSA_IdentityService.Contract.Service.Interface.InternalService;
using ISSA_IdentityService.Core.Config;

namespace ISSA_IdentityService.Service.Services.InternalServices
{
    [SingletonDependency(ServiceType = typeof(IKafkaProducer))]
    public class KafkaProducer : IKafkaProducer
    {
        private readonly IProducer<Null, string> _producer;

        public KafkaProducer()
        {
            var config = new ProducerConfig
            {
                BootstrapServers = SystemSettingModel.Configs?["Kafka:BootstrapServers"] ?? "localhost:9092",
                ClientId = SystemSettingModel.Configs?["Kafka:ClientId"] ?? "ISSA_IdentityService",
                CompressionType = CompressionType.Gzip,
                MessageTimeoutMs = 5000,
                RequestTimeoutMs = 60000
            };

            _producer = new ProducerBuilder<Null, string>(config).Build();
        }

        public void Produce(string topic, string message, CancellationToken cancellationToken = default)
        {
            var kafkamessage = new Message<Null, string> { Value = message, };
            _ = _producer.ProduceAsync(topic, kafkamessage, cancellationToken);
        }
    }
}
