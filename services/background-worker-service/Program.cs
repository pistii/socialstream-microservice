using shared_libraries.Kafka;
using shared_libraries.Kafka.Handlers;
using shared_libraries.Kafka.IServiceClient;
using shared_libraries.Models;

namespace background_worker_service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<IKafkaConsumerHandler<Friend>, FriendHandler>();
                    services.AddHostedService(provider =>
                        new KafkaConsumerService<Friend>(
                            provider.GetRequiredService<ILogger<KafkaConsumerService<Friend>>>(),
                            provider.GetRequiredService<IConfiguration>(),
                            provider.GetRequiredService<IKafkaConsumerHandler<Friend>>(),
                            "getall-friend-topic"));


                    services.AddHttpClient<IUserServiceClient, UserServiceClient>(client =>
                    {
                        client.BaseAddress = new Uri("http://user-service:8080"); // docker-compose-ban definiált service név
                    });
                });
    }
}
