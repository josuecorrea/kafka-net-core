using Confluent.Kafka;
using Kafka.Connector.Contracts;
using Kafka.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kafka.Service.Implements
{
    public class ProducerService : IProducerService
    {
        private readonly IServerConnector _serverConnector;

        public ProducerService(IServerConnector serverConnector)
        {
            _serverConnector = serverConnector;
        }

        public async Task<DeliveryResult<Null, string>> MessagePublish(string topic, string message)
        {
            using var producer = new ProducerBuilder<Null, string>(await _serverConnector.CreateProducerInstanceConnetor()).Build();

            try
            {
                var sendResult = await producer.ProduceAsync(topic, new Message<Null, string> { Value = message },);

                return sendResult;
            }
            catch (ProduceException<Null, string> e)
            {
                throw e;
            }
        }
    }
}
