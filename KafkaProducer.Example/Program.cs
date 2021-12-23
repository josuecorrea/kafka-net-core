using Confluent.Kafka;
using Kafka.Connector.Contracts;
using Kafka.Connector.Implements;
using Kafka.Service.Contracts;
using Kafka.Service.Implements;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace KafkaProducer.Example
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //setup our DI
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .AddSingleton<IConfig, Kafka.Connector.Implements.Config>()
                .AddSingleton<IServerConnector, ServerConnector>()
                .AddSingleton<IServerConnector, ServerConnector>()
                .AddSingleton<IConsumerService, ConsumerService>()
                .AddSingleton<IProducerService, ProducerService>()
                //.AddTransient(typeof(IGenericProducerService<>), typeof(GenericProducerService<>))
                .BuildServiceProvider();

            const string topic = "meutopico";

            //do the actual work here
            var bar = serviceProvider.GetService<IProducerService>();
            while (true) 
            { 
               var result =  bar.MessagePublish(topic, Guid.NewGuid().ToString()).GetAwaiter().GetResult();

                Console.WriteLine($"Result: {Newtonsoft.Json.JsonConvert.SerializeObject(result)}");
            }
        }
    }
}
