using Confluent.Kafka;
using System.Threading.Tasks;

namespace Kafka.Service.Contracts
{
    public interface IProducerService
    {
        Task<DeliveryResult<Null, string>> MessagePublish(string topic, string message);
    }
}
