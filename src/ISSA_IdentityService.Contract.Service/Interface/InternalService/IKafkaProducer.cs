namespace ISSA_IdentityService.Contract.Service.Interface.InternalService
{
    public interface IKafkaProducer
    {
        void Produce(string topic, string message, CancellationToken cancellationToken = default);
    }
}
