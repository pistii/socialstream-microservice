using System.Text.Json;
using Confluent.Kafka;

namespace shared_libraries.Kafka
{
    public class KafkaProducerService<T>
    {
        private readonly IProducer<Null, string> _producer;
        private readonly string _topic;

        public KafkaProducerService(string topic)
        {
            var config = new ProducerConfig { BootstrapServers = "kafka:9092" };
            _producer = new ProducerBuilder<Null, string>(config).Build();
            _topic = topic;
        }

        public async Task ProduceAsync(T message)
        {
            var json = JsonSerializer.Serialize(message);
            await _producer.ProduceAsync(_topic, new Message<Null, string> { Value = json });
        }
    }
}
