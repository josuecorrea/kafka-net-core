using Confluent.Kafka;
using Kafka.Connector.Models;
using System.Threading.Tasks;

namespace Kafka.Connector.Contracts
{
    public interface IConfig
    {
        Task GetProperties();

        Task SetCustomConfig(Models.Configuration configProperties);

        Task<ProducerConfig> CreateProducerConfig();

        Task<ConsumerConfig> CreateConsumerConfig();
    }
}
