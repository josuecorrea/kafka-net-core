using System.Threading;
using System.Threading.Tasks;

namespace Kafka.Service.Contracts
{
    public interface IGenericConsumerService<TKey, TValue>
    {
        Task Consume(string topic, string groupId, CancellationToken stoppingToken);
        
    }
}
