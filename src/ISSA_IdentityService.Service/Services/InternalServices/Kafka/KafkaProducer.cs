using Confluent.Kafka;
using Invedia.DI.Attributes;
using ISSA_IdentityService.Contract.Service.Interface.InternalService;
using ISSA_IdentityService.Core.Config;
using System.Text;

namespace ISSA_IdentityService.Service.Services.InternalServices.Kafka
{
    [SingletonDependency(ServiceType = typeof(IKafkaProducer))]
    public class KafkaProducer : IKafkaProducer
    {
        private readonly IProducer<string, string> _producer;

        public KafkaProducer()
        {
            var config = new ProducerConfig
            {
                BootstrapServers = SystemSettingModel.Configs?["Kafka:BootstrapServers"] ?? "localhost:9092",
                ClientId = SystemSettingModel.Configs?["Kafka:ClientId"] ?? "ISSA_IdentityService",
                CompressionType = CompressionType.Gzip,
                MessageTimeoutMs = 5000,
                RequestTimeoutMs = 60000,
                CompressionLevel = 5,
            };

            _producer = new ProducerBuilder<string, string>(config).Build();
        }

        public void Produce(string topic, string message, CancellationToken cancellationToken = default)
        {
            var kafkamessage = new Message<string, string>
            {
                Key = Guid.NewGuid().ToString(),
                Value = message,
                Timestamp = new Timestamp(DateTime.UtcNow),
                Headers =
                [
                    new Header("MessageId", Encoding.UTF8.GetBytes(Guid.NewGuid().ToString())),
                    new Header("MessageType", Encoding.UTF8.GetBytes("IdentityUpdate"))
                ]
            };
            _ = _producer.ProduceAsync(topic, kafkamessage, cancellationToken);
        }
    }
}
