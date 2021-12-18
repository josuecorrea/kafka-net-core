using System.Threading.Tasks;

namespace Kafka.Service.Contracts
{
    public interface  ICallbackService
    {
        Task<string> Message (string value, long? offset, int? partition, string topic, string topicPartion);
    }
}
