using Confluent.Kafka;
using Kafka.Connector.Implements;
using Kafka.Connector.Models;
using Kafka.Service.Contracts;
using Kafka.Service.Extennsions;
using Microsoft.Extensions.DependencyInjection;
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
                .BuildServiceProvider();

            const string topic = "prod-product-logs";

            var producer = serviceProvider.GetService<IProducerService>();

            while (true)
            {
                var result = await producer.MessagePublish(topic, Guid.NewGuid().ToString());

                Console.WriteLine($"Result: {Newtonsoft.Json.JsonConvert.SerializeObject(result)}");
            }
        }
    }
}
