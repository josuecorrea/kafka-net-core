using System.Threading;
using System.Threading.Tasks;

namespace Kafka.Service.Contracts
{
    public interface IConsumerService
    {
        Task Consume(string topic, string groupId, ICallbackService callback, CancellationToken cancellationToken);
    }
}
