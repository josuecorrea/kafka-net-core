using System;

namespace Kafka.Connector.Models
{
    public class Message
    {
        public Message(Guid correlationId, string messageValue, long offset, int partition, string topic)
        {
            CorrelationId = correlationId;
            MessageValue = messageValue;
            Offset = offset;
            Partition = partition;
            Topic = topic;
        }

        public Guid CorrelationId { get; set; }
        public string MessageValue { get; set; }
        public long Offset { get; set; }
        public int Partition { get; set; }
        public string Topic { get; set; }

    }
}
