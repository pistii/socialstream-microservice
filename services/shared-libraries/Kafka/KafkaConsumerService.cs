using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace shared_libraries.Kafka
{
    public class KafkaConsumerService : BackgroundService
    {
        private readonly ILogger<KafkaConsumerService> _logger;
        private readonly IConsumer<Ignore, string> _consumer;

        public KafkaConsumerService(ILogger<KafkaConsumerService> logger, IConfiguration config)
        {
            _logger = logger;

            var consumerConfig = new ConsumerConfig
            {
                BootstrapServers = config["Kafka:BootstrapServers"],
                GroupId = "friend-consumer-group",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            _consumer = new ConsumerBuilder<Ignore, string>(consumerConfig).Build();
            _consumer.Subscribe("getall-friend-topic");
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.Run(() =>
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    var result = _consumer.Consume(stoppingToken);
                    _logger.LogInformation($"Message received: {result.Message.Value}");
                    Console.WriteLine("request received! ");
                    // Ide jön a feldolgozás logika
                }
            }, stoppingToken);
        }

        public override void Dispose()
        {
            _consumer.Close();
            _consumer.Dispose();
            base.Dispose();
        }
    }

    //public class KafkaConsumerService<T>
    //{
    //    private readonly string _topic;
    //    private readonly IConsumer<Null, string> _consumer;

    //    public KafkaConsumerService(string bootstrapServers, string topic, string groupId)
    //    {
    //        var config = new ConsumerConfig
    //        {
    //            BootstrapServers = bootstrapServers,
    //            GroupId = groupId,
    //            AutoOffsetReset = AutoOffsetReset.Earliest
    //        };

    //        _consumer = new ConsumerBuilder<Null, string>(config).Build();
    //        _topic = topic;
    //    }

    //    public void StartConsuming(Func<T, Task> onMessageReceived)
    //    {
    //        _consumer.Subscribe(_topic);

    //        Task.Run(() =>
    //        {
    //            while (true)
    //            {
    //                var result = _consumer.Consume();
    //                var message = JsonSerializer.Deserialize<T>(result.Message.Value);
    //                onMessageReceived(message).Wait(); // Exception handling, retry később
    //                Console.WriteLine("message received");
    //                Console.WriteLine(message);
    //            }
    //        });
    //    }
    //}

}
