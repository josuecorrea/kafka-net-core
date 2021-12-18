using Confluent.Kafka;
using System.Threading.Tasks;

namespace Kafka.Connector.Contracts
{
    public interface IServerConnector
    {
        Task<ProducerConfig> CreateProducerInstanceConnetor();

        Task<ConsumerConfig> CreateConsumerInstanceConnetor();
    }
}
