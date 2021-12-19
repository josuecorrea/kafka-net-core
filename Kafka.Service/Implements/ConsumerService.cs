﻿using Confluent.Kafka;
using Kafka.Connector.Contracts;
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

        public ConsumerService(IServerConnector serverConnectorFactory)
        {
            _serverConnectorFactory = serverConnectorFactory;
        }

        public async Task Consume(string topic, string groupId, ICallbackService callback, CancellationToken cancellationToken)
        {
            var instanceConnetor = await _serverConnectorFactory.CreateConsumerInstanceConnetor();
            instanceConnetor.GroupId = groupId;

            using var consumer = new ConsumerBuilder<Ignore, string>(instanceConnetor).Build();
            
            consumer.Subscribe(topic);

            var cts = new CancellationTokenSource();

            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    var message = consumer.Consume(cts.Token);
                    
                    if (message != null)
                    {
                        await callback.Message(message.Message.Value, message.Offset.Value, message.Partition.Value, message.Topic, message.TopicPartition.Topic);

                        if (!true) //if (!_commitOnConsume)//TODO: OPÇÃO DEVE VIR DO CONFIG
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

        public async Task ConsumePartitionOffSet(string topic, string groupId, int partition, long offset, ICallbackService callback, CancellationToken cancellationToken)
        {
            var instanceConnetor = await _serverConnectorFactory.CreateConsumerInstanceConnetor();
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
                        await callback.Message(message.Message.Value, message.Offset.Value, message.Partition.Value, message.Topic, message.TopicPartition.Topic);

                        if (!true) //if (!_commitOnConsume)//TODO: OPÇÃO DEVE VIR DO CONFIG
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
            var instanceConnetor = await _serverConnectorFactory.CreateConsumerInstanceConnetor();
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
                        await callback.Message(message.Message.Value, message.Offset.Value, message.Partition.Value, message.Topic, message.TopicPartition.Topic);

                        if (!true) //if (!_commitOnConsume)//TODO: OPÇÃO DEVE VIR DO CONFIG
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

        public async Task<IEnumerable<TopicPartitionOffset>> GetCurrentCommitedOffSetFromTopic(string topic, string groupId, int partition)
        {
            var instanceConnetor = await _serverConnectorFactory.CreateConsumerInstanceConnetor();
            instanceConnetor.GroupId = groupId;

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


        public async Task<Offset> GetCurrentOffSetPositionFromTopic(string topic, string groupId, int partition)
        {
            var instanceConnetor = await _serverConnectorFactory.CreateConsumerInstanceConnetor();
            instanceConnetor.GroupId = groupId;

            using var consumer = new ConsumerBuilder<Ignore, string>(instanceConnetor).Build();

            consumer.Subscribe(topic);

            var topicPartition = new TopicPartition(topic, partition);   

            var result = consumer.Position(topicPartition);

            return result;
        }


        public async Task StoreOffsets(string topic, long offset, int partition)
        {
            var instanceConnetor = await _serverConnectorFactory.CreateConsumerInstanceConnetor();
            
            using var consumer = new ConsumerBuilder<Ignore, string>(instanceConnetor).Build();

            consumer.Subscribe(topic);

            var topicPartitionOffset = new TopicPartitionOffset(topic, partition, offset);

            consumer.StoreOffset(topicPartitionOffset);
        }
    }
}
