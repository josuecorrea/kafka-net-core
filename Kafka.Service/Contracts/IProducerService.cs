using Confluent.Kafka;
using Confluent.Kafka.Admin;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kafka.Service.Contracts
{
    public interface IProducerService
    {
        Task<DeliveryResult<Null, string>> MessagePublish(string topic, string message);
        Task<DeliveryResult<Null, string>> MessagePublish(string topic, int partition, string message);
        Task CreateTopics(IEnumerable<TopicSpecification> topicsSpecification);
        Task DeleteTopics(IEnumerable<string> topics);
        Task<IEnumerable<string>> ListAllTopics();
        Task<bool> CheckTopicExistsByName(string topicName);
    }
}
