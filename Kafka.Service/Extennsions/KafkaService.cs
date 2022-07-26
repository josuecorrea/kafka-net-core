using Kafka.Connector.Contracts;
using Kafka.Connector.Implements;
using Kafka.Service.Contracts;
using Kafka.Service.Implements;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Kafka.Service.Extennsions
{
    public static class KafkaService
    {
        public static IServiceCollection AddKafkaService(this IServiceCollection services, ConfigOptions config)
        {
            config.ExecuteServerConfigCheck();

            services.AddSingleton<IConfigOptions, ConfigOptions>();
            services.AddSingleton<IServerConnector, ServerConnector>();
            services.AddSingleton<IConsumerService, ConsumerService>();
            services.AddSingleton<IProducerService, ProducerService>();
            services.AddTransient(typeof(IGenericProducerService<,>), typeof(GenericProducerService<,>));
            services.AddTransient(typeof(IGenericConsumerService<,>), typeof(GenericConsumerService<,>));

            return services;
        }      
    }
}
