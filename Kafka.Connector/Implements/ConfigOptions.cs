using Confluent.Kafka;
using Kafka.Connector.Contracts;
using Kafka.Connector.Models;
using System;
using System.Threading.Tasks;

namespace Kafka.Connector.Implements
{
    public class ConfigOptions : IConfig
    {
        public static BasicConfiguration CustomConfig { get; set; }
        public static ProducerConfig ProducerConfig { get; set; }
        public static ConsumerConfig ConsumerConfig { get; set; }

        public async Task ExecuteServerConfig()
        {
            await CreateProducerConfig();
            await CreateConsumerConfig();
        }

        private async Task<ProducerConfig> CreateProducerConfig()
        {
           var config = new ProducerConfig();

            await Task.Run(() =>
            {
                config.BootstrapServers = "localhost:9092";
                //config.SslCaLocation = "/Path-to/cluster-ca-certificate.pem";
                //config.SecurityProtocol = SecurityProtocol.SaslSsl;
                //config.SaslMechanism = SaslMechanism.ScramSha256;
                //config.SaslUsername = "ickafka";
                //config.SaslPassword = "yourpassword";
                //config.Acks = Acks.Leader; //TODO: COLOCAR COMO  FEATURE FLAG
                
            });
            
            return config;
        }

        private async Task<ConsumerConfig> CreateConsumerConfig()
        {
            var config = new ConsumerConfig();

            await Task.Run(() =>
            {
                config.BootstrapServers = "localhost:9092";
                //config.SecurityProtocol = SecurityProtocol.SaslPlaintext;
                //config.SaslMechanism = SaslMechanism.ScramSha256;
                //config.SaslUsername = "ickafka";
                //config.SaslPassword = "yourpassword";
                //config.AutoOffsetReset = AutoOffsetReset.Earliest;
                //config.Acks = Acks.Leader; //TODO: COLOCAR COMO  FEATURE FLAG               

            });

            return config;
        }
    }
}
