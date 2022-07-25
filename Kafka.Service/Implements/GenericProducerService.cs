using Confluent.Kafka;
using Kafka.Connector;
using Kafka.Connector.Contracts;
using Kafka.Service.Contracts;
using System;
using System.Threading.Tasks;

namespace Kafka.Service.Implements
{

    public class GenericProducerService<TKey, TValue> : IDisposable, IGenericProducerService<TKey, TValue> where TValue : class
    {
        private readonly IProducer<TKey, TValue> _producer;

        private readonly IServerConnector _serverConnectorFactory;

        public GenericProducerService(IServerConnector serverConnectorFactory)
        {
            _serverConnectorFactory = serverConnectorFactory;

            _producer = new ProducerBuilder<TKey, TValue>(_serverConnectorFactory.GetProducerInstanceConnetor().GetAwaiter().GetResult()).SetValueSerializer(new JsonSerializer<TValue>()).Build();
        }
       
        public async Task ProduceAsync(string topic, TKey key, TValue value)
        {
            await _producer.ProduceAsync(topic, new Message<TKey, TValue> { Key = key, Value = value });
        }

        public void Dispose()
        {
            _producer.Flush();
            _producer.Dispose();
        }
    }
}
