using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kafka.Service.Contracts
{
    internal interface IGenericProducerService<TKey, TValue>
    {
        Task ProduceAsync(string topic, TKey key, TValue value);
    }
}
