using System;

namespace Kafka.Connector.Models
{
    public class Mesage<T>
    {
        public Mesage(Guid correlationId, T payload)
        {
            CorrelationId = correlationId;
            Payload = payload;
        }

        public Guid CorrelationId { get; set; }
        public T Payload { get; set; }
    }
}
