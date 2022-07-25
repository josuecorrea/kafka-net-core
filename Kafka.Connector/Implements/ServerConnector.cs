using Confluent.Kafka;
using Kafka.Connector.Contracts;
using System.Threading.Tasks;

namespace Kafka.Connector.Implements
{
    public class ServerConnector : ConfigOptions, IServerConnector
    {
        private readonly IConfig _config;

        public ServerConnector(IConfig config)
        {
            _config = config;
        }

        public async Task<ConsumerConfig> GetConsumerInstanceConnetor() => ConsumerConfig;

        public async Task<ProducerConfig> GetProducerInstanceConnetor() => ProducerConfig;
    }
}
