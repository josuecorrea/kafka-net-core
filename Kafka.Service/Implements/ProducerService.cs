using Confluent.Kafka;
using Confluent.Kafka.Admin;
using Kafka.Connector.Contracts;
using Kafka.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kafka.Service.Implements
{
    public class ProducerService : IProducerService, IDisposable
    {
        private readonly IServerConnector _serverConnectorFactory;
        readonly IProducer<Null, string> _producer;
        private readonly IAdminClient adminClient;

        public ProducerService(IServerConnector serverConnectorFactory)
        {
            _serverConnectorFactory = serverConnectorFactory;

            _producer = new ProducerBuilder<Null, string>(_serverConnectorFactory.GetProducerInstanceConnetor().GetAwaiter().GetResult()).Build();

            adminClient = new AdminClientBuilder(new List<KeyValuePair<string, string>>
            {

            })
            .Build();
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

        public async Task CreateTopics(IEnumerable<TopicSpecification> topicsSpecification)
        {
            try
            {
                try
                {
                    await adminClient.CreateTopicsAsync(topicsSpecification);
                }
                catch (CreateTopicsException e)
                {
                    throw;
                }
            }
            catch (ProduceException<Null, string> e)
            {
                throw e;
            }
        }

        public async Task DeleteTopics(IEnumerable<string> topics)
        {
            try
            {
                try
                {
                    await adminClient.DeleteTopicsAsync(topics);
                }
                catch (CreateTopicsException e)
                {
                    throw;
                }
            }
            catch (ProduceException<Null, string> e)
            {
                throw e;
            }
        }

        public async Task<IEnumerable<string>> ListAllTopics()
        {
            try
            {
                try
                {
                    var topics = new List<string>();

                    var meta = adminClient.GetMetadata(TimeSpan.FromSeconds(1));

                    meta.Topics.ForEach(c => topics.Add(c.Topic));

                    return topics;
                }
                catch (CreateTopicsException e)
                {
                    throw;
                }
            }
            catch (ProduceException<Null, string> e)
            {
                throw e;
            }
        }

        public async Task<bool> CheckTopicExistsByName(string topicName)
        {
            try
            {
                try
                {
                    var topics = new List<string>();

                    var meta = adminClient.GetMetadata(TimeSpan.FromSeconds(1));

                    var topicFiltered = meta.Topics.Where(c => c.Topic.Equals(topicName)).ToList();

                    return topicFiltered.Any();
                }
                catch (CreateTopicsException e)
                {
                    throw;
                }
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

            GC.SuppressFinalize(this);
        }
    }
}
