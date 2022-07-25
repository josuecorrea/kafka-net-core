using Confluent.Kafka;
using Kafka.Connector.Contracts;
using Kafka.Connector.Models;
using System;
using System.Threading.Tasks;

namespace Kafka.Connector.Implements
{
    public class ConfigOptions : IConfig
    {
        public ConfigOptions(ConfigServerProperties configServerProperties,
                             ProducerConfig producerConfig,
                             ConsumerConfig consumerConfig)
        {
            ConfigServerProperties = configServerProperties;
            ProducerConfig = producerConfig;
            ConsumerConfig = consumerConfig;
        }

        protected ConfigOptions() { }

        public static ConfigServerProperties ConfigServerProperties { get; private set; }
        public static ProducerConfig ProducerConfig { get; private set; }
        public static ConsumerConfig ConsumerConfig { get; private set; }

        public async Task ExecuteServerConfigCheck()
        {
            await CheckProducerConfig();
            await CheckConsumerConfig();
        }

        private async Task CheckProducerConfig()
        {
            await Task.Run(() =>
            {
                if (String.IsNullOrEmpty(ProducerConfig.BootstrapServers))
                {
                    ProducerConfig.BootstrapServers = ConfigServerProperties.BootstrapServers;
                }
                //config.SslCaLocation = "/Path-to/cluster-ca-certificate.pem";
                //config.SecurityProtocol = SecurityProtocol.SaslSsl;
                //config.SaslMechanism = SaslMechanism.ScramSha256;
                //config.SaslUsername = "ickafka";
                //config.SaslPassword = "yourpassword";
                //config.Acks = Acks.Leader; //TODO: COLOCAR COMO  FEATURE FLAG

            });           
        }

        private async Task CheckConsumerConfig()
        {
            await Task.Run(() =>
            {
                if (String.IsNullOrEmpty(ConsumerConfig.BootstrapServers))
                {
                    ConsumerConfig.BootstrapServers = ConfigServerProperties.BootstrapServers;
                }
                //config.SecurityProtocol = SecurityProtocol.SaslPlaintext;
                //config.SaslMechanism = SaslMechanism.ScramSha256;
                //config.SaslUsername = "ickafka";
                //config.SaslPassword = "yourpassword";
                //config.AutoOffsetReset = AutoOffsetReset.Earliest;
                //config.Acks = Acks.Leader; //TODO: COLOCAR COMO  FEATURE FLAG               

            });          
        }
    }
}
