
using Microsoft.Extensions.Hosting;

namespace shared_libraries.Kafka
{
    public class FriendKafkaConsumer : BackgroundService
    {
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            throw new NotImplementedException();
        }
    }

    public interface IFriendKafkaConsumer
    {

    }
}
