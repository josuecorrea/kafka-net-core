using Confluent.Kafka;
using Kafka.Connector.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kafka.Connector.Contracts
{
    public interface IConfig
    {
        Task GetProperties();

        Task SetCustomConfig(ConfigProperties configProperties);

        Task<ProducerConfig> CreateProducerConfig();

        Task<ConsumerConfig> CreateConsumerConfig();
    }
}
