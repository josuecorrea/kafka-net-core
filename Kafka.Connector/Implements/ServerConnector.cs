using Confluent.Kafka;
using Kafka.Connector.Contracts;
using System.Threading.Tasks;

namespace Kafka.Connector.Implements
{
    public class ServerConnector : ConfigOptions, IServerConnector
    {      
        public ServerConnector() { }

        public async Task<ConsumerConfig> GetConsumerInstanceConnetor() => ConsumerConfig;

        public async Task<ProducerConfig> GetProducerInstanceConnetor() => ProducerConfig;
    }
}
