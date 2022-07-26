using Confluent.Kafka;
using Kafka.Connector.Implements;
using Kafka.Connector.Models;
using Kafka.Service.Contracts;
using Kafka.Service.Extennsions;
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
                .AddKafkaService(new ConfigOptions(true,
                     new ConfigServerProperties
                     {
                         BootstrapServers = "localhost:9092"                         
                     },
                    new ProducerConfig
                    {
                        //Acks = Acks.All
                    },
                    new ConsumerConfig
                    {
                        //Acks = Acks.All
                    }))
                .AddTransient<ICallbackService, CallBack>()
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

