﻿using Kafka.Connector.Contracts;
using Kafka.Connector.Implements;
using Kafka.Service.Contracts;
using Kafka.Service.Implements;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KafkaConsumer.Example
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .AddSingleton<IConfig, Kafka.Connector.Implements.Config>()
                .AddSingleton<IServerConnector, ServerConnector>()
                .AddSingleton<IServerConnector, ServerConnector>()
                .AddSingleton<IConsumerService, ConsumerService>()
                .AddSingleton<ICallbackService, CallBack>()
                //.AddSingleton<IProducerService, ProducerService>()
                //.AddTransient(typeof(IGenericProducerService<>), typeof(GenericProducerService<>))
                .BuildServiceProvider();


            const string topic = "meutopico";

            var cts = new CancellationToken();

            var consumer = serviceProvider.GetService<IConsumerService>();
            var callBack = serviceProvider.GetService<ICallbackService>();

            await consumer.Consume(topic, "consumidor", callBack, cts);//.GetAwaiter().GetResult();

            Console.ReadKey();
        }
    }
}

