using Confluent.Kafka;
using System.Threading.Tasks;

namespace Kafka.Connector.Contracts
{
    public interface IConfigOptions
    {
        Task<bool> IsAutoCommit();
        Task<ProducerConfig> GetProducerConfig();
        Task<ConsumerConfig> GetConsumerConfig();
    }
}
