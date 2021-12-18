using Confluent.Kafka;
using Kafka.Connector.Contracts;
using Kafka.Connector.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace Kafka.Connector.Implements
{
    public class Config : IConfig
    {
        private readonly IConfiguration _configuration;

        public Config(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public Task GetProperties()
        {
            throw new NotImplementedException();
        }

        public Task SetCustomConfig(ConfigProperties configProperties)
        {
            throw new NotImplementedException();
        }

        public async Task<ProducerConfig> CreateProducerConfig()
        {
            var config = new ProducerConfig();

            await Task.Run(() =>
            {
                config.BootstrapServers = "<your-IP-port-pairs>";
                config.SslCaLocation = "/Path-to/cluster-ca-certificate.pem";
                config.SecurityProtocol = SecurityProtocol.SaslSsl;
                config.SaslMechanism = SaslMechanism.ScramSha256;
                config.SaslUsername = "ickafka";
                config.SaslPassword = "yourpassword";
            });
            
            return config;
        }

        public async Task<ConsumerConfig> CreateConsumerConfig()
        {
            var config = new ConsumerConfig();

            await Task.Run(() =>
            {
                config.BootstrapServers = "<your-IP-port-pairs>";
                config.SecurityProtocol = SecurityProtocol.SaslPlaintext;
                config.SaslMechanism = SaslMechanism.ScramSha256;
                config.SaslUsername = "ickafka";
                config.SaslPassword = "yourpassword";
                config.AutoOffsetReset = AutoOffsetReset.Earliest;
                
            });

            return config;
        }
    }
}
