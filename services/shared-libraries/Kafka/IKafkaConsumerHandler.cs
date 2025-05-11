
namespace shared_libraries.Kafka
{
    public interface IKafkaConsumerHandler<T>
    {
        Task HandleAsync(T message, CancellationToken cancellationToken);
    }
}
