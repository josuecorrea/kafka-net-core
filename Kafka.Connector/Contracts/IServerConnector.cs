using Confluent.Kafka;
using System.Threading.Tasks;

namespace Kafka.Connector.Contracts
{
    public interface IServerConnector
    {
        Task<ProducerConfig> GetProducerInstanceConnetor();

        Task<ConsumerConfig> GetConsumerInstanceConnetor();
    }
}
