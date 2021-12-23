using Confluent.Kafka;
using Kafka.Connector.Contracts;
using Kafka.Connector.Implements;
using Kafka.Service.Contracts;
using Kafka.Service.Implements;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace KafkaProducer.Example
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
                .AddSingleton<IProducerService, ProducerService>()
                //.AddTransient(typeof(IGenericProducerService<>), typeof(GenericProducerService<>))
                .BuildServiceProvider();

            const string topic = "meutopico";

            var producer = serviceProvider.GetService<IProducerService>();
            while (true) 
            {
                var result = await producer.MessagePublish(topic, Guid.NewGuid().ToString());

                Console.WriteLine($"Result: {Newtonsoft.Json.JsonConvert.SerializeObject(result)}");
            }
        }
    }
}
