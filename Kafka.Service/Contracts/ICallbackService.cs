
using Kafka.Connector.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kafka.Service.Contracts
{
    public interface  ICallbackService
    {
        Task Message (Message message);
        Task Message (IEnumerable<Message> messages);
    }
}
