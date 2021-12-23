using System.Threading.Tasks;

namespace Kafka.Service.Contracts
{
    public interface IGenericProducerService<TKey, TValue>
    {
        Task ProduceAsync(string topic, TKey key, TValue value);
    }
}
