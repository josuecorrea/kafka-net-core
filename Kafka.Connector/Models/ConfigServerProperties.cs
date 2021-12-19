namespace Kafka.Connector.Models
{
    public class ConfigServerProperties
    {
        public string BootstrapServers { get; set; }
        public string SslCaLocation    { get; set; }
        public string SecurityProtocol { get; set; }
        public string SaslMechanism    { get; set; }
        public string SaslUsername     { get; set; }
        public string SaslPassword { get; set; }
    }
}
