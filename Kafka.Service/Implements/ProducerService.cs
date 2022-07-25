using Confluent.Kafka;
using Kafka.Connector.Contracts;
using Kafka.Service.Contracts;
using System;
using System.Threading.Tasks;

namespace Kafka.Service.Implements
{
    public class ProducerService : IProducerService, IDisposable
    {
        private readonly IServerConnector _serverConnectorFactory;
        readonly IProducer<Null, string> _producer;

        public ProducerService(IServerConnector serverConnectorFactory)
        {
            _serverConnectorFactory = serverConnectorFactory;

            _producer = new ProducerBuilder<Null, string>(_serverConnectorFactory.GetProducerInstanceConnetor().GetAwaiter().GetResult()).Build();
        }

        public async Task<DeliveryResult<Null, string>> MessagePublish(string topic, string message)
        {
            try
            {
                var sendResult = await _producer.ProduceAsync(topic, new Message<Null, string> { Value = message });

                return sendResult;
            }
            catch (ProduceException<Null, string> e)
            {
                throw e;
            }
        }

        public async Task<DeliveryResult<Null, string>> MessagePublish(string topic, int partition, string message)
        {
            try
            {
                var topicPartition = new TopicPartition(topic, partition);

                var sendResult = await _producer.ProduceAsync(topicPartition, new Message<Null, string> { Value = message });

                return sendResult;
            }
            catch (ProduceException<Null, string> e)
            {
                throw e;
            }
        }

        public void Dispose()
        {
            _producer.Flush();
            _producer.Dispose();
        }
    }
}
