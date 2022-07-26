using Confluent.Kafka;
using Kafka.Connector.Contracts;
using System.Threading.Tasks;

namespace Kafka.Connector.Implements
{
    public class ServerConnector :  IServerConnector
    {
        private readonly IConfigOptions _configOptions;

        public ServerConnector(IConfigOptions configOptions)
        {
            _configOptions = configOptions;
        }

        public async Task<ConsumerConfig> GetConsumerInstanceConnetor() => await _configOptions.GetConsumerConfig();

        public async Task<ProducerConfig> GetProducerInstanceConnetor() => await _configOptions.GetProducerConfig();
    }
}
