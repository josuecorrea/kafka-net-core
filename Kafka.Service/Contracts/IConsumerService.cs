using Confluent.Kafka;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Kafka.Service.Contracts
{
    public interface IConsumerService
    {
        Task Consume(string topic, string groupId, ICallbackService callback, CancellationToken cancellationToken);
        Task Consume(TopicPartition topicPartition, string groupId, ICallbackService callback, CancellationToken cancellationToken);
        Task Consume(TopicPartition topicPartition, string groupId, ICallbackService callback, long quantity, CancellationToken cancellationToken);
        Task Consume(string topic, string groupId, ICallbackService callback, long quantity, CancellationToken cancellationToken);
        Task<IEnumerable<TopicPartitionOffset>> GetCurrentCommitedOffSetFromTopic(string topic, int partition);
        Task StoreOffsets(string topic, long offset, int partition);
        Task OffSetConsumer(string topic, string groupId, int partition, long offset, ICallbackService callback, CancellationToken cancellationToken);
        Task<Offset> GetCurrentOffSetPositionFromTopic(string topic, int partition);
    }
}
