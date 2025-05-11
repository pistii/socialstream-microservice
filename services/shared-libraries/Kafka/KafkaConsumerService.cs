using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using shared_libraries.Kafka.IServiceClient;
using shared_libraries.Models;
using System.Text.Json;

namespace shared_libraries.Kafka
{
    public class KafkaConsumerService<T> : BackgroundService
    {
        private readonly ILogger<KafkaConsumerService<T>> _logger;
        private readonly IConsumer<Ignore, string> _consumer;
        private readonly IKafkaConsumerHandler<T> _handler;
        private string _topic;
        public KafkaConsumerService(
            ILogger<KafkaConsumerService<T>> logger, 
            IConfiguration config, 
            IKafkaConsumerHandler<T> kafkaConsumerHandler, 
            string topic)
        {
            _logger = logger;
            _handler = kafkaConsumerHandler;
            _topic = topic;

            var consumerConfig = new ConsumerConfig
            {
                BootstrapServers = config["Kafka:BootstrapServers"],
                GroupId = $"consumer-group-{typeof(T).Name.ToLower()}",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            _consumer = new ConsumerBuilder<Ignore, string>(consumerConfig).Build();
            _consumer.Subscribe(_topic);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.Run(async () =>
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    try
                    {
                        var result = _consumer.Consume(stoppingToken);
                        var message = JsonSerializer.Deserialize<T>(result.Message.Value);
                        if (message != null)
                        {
                            await _handler.HandleAsync(message, stoppingToken);
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, $"Error processing message on topic '{_topic}'");
                    }
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


}
