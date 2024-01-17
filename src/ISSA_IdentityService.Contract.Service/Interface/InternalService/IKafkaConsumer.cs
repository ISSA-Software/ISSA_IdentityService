namespace ISSA_IdentityService.Contract.Service.Interface.InternalService
{
    public interface IKafkaConsumer
    {
        Task Consume(CancellationToken cancellationToken = default);
    }
}
