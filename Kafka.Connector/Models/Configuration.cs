namespace Kafka.Connector.Models
{
    public class BasicConfiguration
    {
        public ConfigServerProperties Properties { get; set; }
        public bool AcksEnabled { get; set; }
        public bool CommitOnConsume { get; set; }
    }
}
