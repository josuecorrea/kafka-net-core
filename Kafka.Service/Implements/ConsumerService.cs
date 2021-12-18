using Confluent.Kafka;
using Kafka.Connector.Contracts;
using Kafka.Service.Contracts;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Kafka.Service.Implements
{
    public class ConsumerService : IConsumerService
    {

        private readonly IServerConnector _serverConnectorFactory;        

        public ConsumerService(IServerConnector serverConnector)
        {
            _serverConnectorFactory = serverConnector;
        }

        public async Task Start(string topic, string groupId, ICallbackService callback, CancellationToken cancellationToken)
        {
            var instanceConnetor = await _serverConnectorFactory.CreateConsumerInstanceConnetor();
            instanceConnetor.GroupId = groupId;

            using var consumer = new ConsumerBuilder<Ignore, string>(instanceConnetor).Build();
            consumer.Subscribe(topic);            

            var cts = new CancellationTokenSource();

            try
            {
                while (true)
                {
                    var message = consumer.Consume(cts.Token);

                    await callback.Message(message.Message.Value, message.Offset.Value, message.Partition.Value, message.Topic, message.TopicPartition.Topic);
                }
            }
            catch (OperationCanceledException)
            {
                consumer.Close();
            }
        }
    }
}

