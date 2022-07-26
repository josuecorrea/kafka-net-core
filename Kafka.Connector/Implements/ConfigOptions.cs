using Confluent.Kafka;
using Kafka.Connector.Contracts;
using Kafka.Connector.Models;
using System;
using System.Threading.Tasks;

namespace Kafka.Connector.Implements
{
    public class ConfigOptions : IConfigOptions
    {
        public ConfigOptions(bool autoCommit,
                             ConfigServerProperties configServerProperties,
                             ProducerConfig producerConfig,
                             ConsumerConfig consumerConfig)
        {
            ConfigServerProperties = configServerProperties;
            ProducerConfig = producerConfig;
            ConsumerConfig = consumerConfig;
            AutoCommit = autoCommit;
        }

        public ConfigOptions() { }

        public static bool AutoCommit { get; private set; }
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

        public async Task<bool> IsAutoCommit()
        {
            return AutoCommit;
        }

        public async Task<ProducerConfig> GetProducerConfig()
        {
            return ProducerConfig;
        }

        public async Task<ConsumerConfig> GetConsumerConfig()
        {
            return ConsumerConfig;
        }
    }
}
