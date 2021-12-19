namespace Kafka.Connector.Models
{
    public class Configuration
    {
        public ConfigServerProperties Properties { get; set; }
        public bool AcksEnabled { get; set; }
        public bool CommitOnConsume { get; set; }
    }
}
