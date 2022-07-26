using Confluent.Kafka;
using Kafka.Connector.Contracts;
using Kafka.Connector.Models;
using Kafka.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Kafka.Service.Implements
{
    public class ConsumerService : IConsumerService
    {
        private readonly IServerConnector _serverConnectorFactory;
        private readonly IConfigOptions _config;

        public ConsumerService(IServerConnector serverConnectorFactory, IConfigOptions config)
        {
            _serverConnectorFactory = serverConnectorFactory;
            _config = config;
        }

        public async Task Consume(string topic, string groupId, ICallbackService callback, CancellationToken cancellationToken)
        {
            var instanceConnetor = await _serverConnectorFactory.GetConsumerInstanceConnetor();
            instanceConnetor.GroupId = groupId;
            
            using var consumer = new ConsumerBuilder<Ignore, string>(instanceConnetor).Build();

            consumer.Subscribe(topic);
            
            var cts = new CancellationTokenSource();

            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    var message = consumer.Consume(5000);

                    if (message != null)
                    {                        
                        await callback.Message(new Connector.Models.Message(Guid.NewGuid(), message.Message.Value, message.Offset.Value, message.Partition.Value, message.Topic));

                        if (await _config.IsAutoCommit())
                        {
                            consumer.Commit(message);
                        }
                    }
                }
            }
            catch (OperationCanceledException ex)
            {
                consumer.Close();
            }
        }

        public async Task Consume(TopicPartition topicPartition, string groupId, ICallbackService callback, CancellationToken cancellationToken)
        {
            var instanceConnetor = await _serverConnectorFactory.GetConsumerInstanceConnetor();
            instanceConnetor.GroupId = groupId;

            using var consumer = new ConsumerBuilder<Ignore, string>(instanceConnetor).Build();

            consumer.Assign(topicPartition);

            var cts = new CancellationTokenSource();

            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    var message = consumer.Consume(5000);

                    if (message != null)
                    {
                        await callback.Message(new Connector.Models.Message(Guid.NewGuid(), message.Message.Value, message.Offset.Value, message.Partition.Value, message.Topic));

                        if (await _config.IsAutoCommit())
                        {
                            consumer.Commit(message);
                        }
                    }
                }
            }
            catch (OperationCanceledException ex)
            {
                consumer.Close();
            }
        }


        public async Task Consume(string topic, string groupId, ICallbackService callback, long quantity, CancellationToken cancellationToken)
        {
            var instanceConnetor = await _serverConnectorFactory.GetConsumerInstanceConnetor();
            instanceConnetor.GroupId = groupId;

            using var consumer = new ConsumerBuilder<Ignore, string>(instanceConnetor).Build();

            consumer.Subscribe(topic);

            var cts = new CancellationTokenSource();

            var messages =  new List<Message>();

            try
            {
                for (int i = 0; i <= quantity; i++)
                {
                    var message = consumer.Consume(cts.Token);

                    if (message != null)
                    {
                         messages.Add(new Connector.Models.Message(Guid.NewGuid(), message.Message.Value, message.Offset.Value, message.Partition.Value, message.Topic));

                        if (await _config.IsAutoCommit())
                        {
                            consumer.Commit(message);
                        }
                    }
                }  
                
                await callback.Message(messages);
            }
            catch (OperationCanceledException)
            {
                consumer.Close();
            }
        }

        public async Task Consume(TopicPartition topicPartition, string groupId, ICallbackService callback, long quantity, CancellationToken cancellationToken)
        {
            var instanceConnetor = await _serverConnectorFactory.GetConsumerInstanceConnetor();
            instanceConnetor.GroupId = groupId;

            using var consumer = new ConsumerBuilder<Ignore, string>(instanceConnetor).Build();

            consumer.Assign(topicPartition);

            var cts = new CancellationTokenSource();

            var messages = new List<Message>();

            try
            {
                for (int i = 0; i <= quantity; i++)
                {
                    var message = consumer.Consume(cts.Token);

                    if (message != null)
                    {
                        messages.Add(new Connector.Models.Message(Guid.NewGuid(), message.Message.Value, message.Offset.Value, message.Partition.Value, message.Topic));

                        if (await _config.IsAutoCommit())
                        {
                            consumer.Commit(message);
                        }
                    }
                }

                await callback.Message(messages);
            }
            catch (OperationCanceledException)
            {
                consumer.Close();
            }
        }


        public async Task AssignNewPartitionOffSet(string topic, string groupId, int partition, long offset, ICallbackService callback, CancellationToken cancellationToken)
        {
            var instanceConnetor = await _serverConnectorFactory.GetConsumerInstanceConnetor();
            instanceConnetor.GroupId = groupId;

            using var consumer = new ConsumerBuilder<Ignore, string>(instanceConnetor).Build();

            consumer.Subscribe(topic);

            consumer.Assign(new TopicPartitionOffset(topic, partition, offset));

            var cts = new CancellationTokenSource();

            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    var message = consumer.Consume(cts.Token);

                    if (message != null)
                    {
                        await callback.Message(new Connector.Models.Message(Guid.NewGuid(), message.Message.Value, message.Offset.Value, message.Partition.Value, message.Topic));

                        if (await _config.IsAutoCommit())
                        {
                            consumer.Commit(message);                           
                        }
                    }
                }
            }
            catch (OperationCanceledException)
            {
                consumer.Close();
            }
        }

        public async Task OffSetConsumer(string topic, string groupId, int partition, long offset, ICallbackService callback, CancellationToken cancellationToken)
        {
            var instanceConnetor = await _serverConnectorFactory.GetConsumerInstanceConnetor();
            instanceConnetor.GroupId = groupId;

            using var consumer = new ConsumerBuilder<Ignore, string>(instanceConnetor).Build();

            consumer.Subscribe(topic);

            var topicPartition = new TopicPartitionOffset(topic, partition, offset);

            var topicPartitionsOffsets = new List<TopicPartitionOffset>()
                    {
                        topicPartition,
                    };

            consumer.IncrementalAssign(topicPartitionsOffsets);

            var cts = new CancellationTokenSource();

            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    var message = consumer.Consume(cts.Token);

                    if (message != null)
                    {
                        await callback.Message(new Connector.Models.Message(Guid.NewGuid(), message.Message.Value, message.Offset.Value, message.Partition.Value, message.Topic));

                        if (await _config.IsAutoCommit())
                        {
                            consumer.Commit(message);
                        }
                    }
                }
            }
            catch (OperationCanceledException)
            {
                consumer.Close();
            }
        }

        public async Task<IEnumerable<TopicPartitionOffset>> GetCurrentCommitedOffSetFromTopic(string topic, int partition)
        {
            var instanceConnetor = await _serverConnectorFactory.GetConsumerInstanceConnetor();

            using var consumer = new ConsumerBuilder<Ignore, string>(instanceConnetor).Build();

            consumer.Subscribe(topic);

            var topicPartition = new TopicPartition(topic, partition);

            var topicPartitions = new List<TopicPartition>()
                    {
                        topicPartition,
                    };

            var result = consumer.Committed(topicPartitions, TimeSpan.FromSeconds(10));

            return result;           
        }

        public async Task<Offset> GetCurrentOffSetPositionFromTopic(string topic, int partition)
        {
            var instanceConnetor = await _serverConnectorFactory.GetConsumerInstanceConnetor();

            using var consumer = new ConsumerBuilder<Ignore, string>(instanceConnetor).Build();

            consumer.Subscribe(topic);

            var topicPartition = new TopicPartition(topic, partition);   

            var result = consumer.Position(topicPartition);

            return result;
        }

        public async Task StoreOffsets(string topic, long offset, int partition)
        {
            var instanceConnetor = await _serverConnectorFactory.GetConsumerInstanceConnetor();
            
            using var consumer = new ConsumerBuilder<Ignore, string>(instanceConnetor).Build();

            consumer.Subscribe(topic);

            var topicPartitionOffset = new TopicPartitionOffset(topic, partition, offset);

            consumer.StoreOffset(topicPartitionOffset);
        }
    }
}

