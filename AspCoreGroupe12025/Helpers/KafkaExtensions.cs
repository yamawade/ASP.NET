
using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AspCoreGroupe12025.Services;
using AspCoreGroupe12025.HostedServices;


namespace AspCoreGroupe12025.Helpers
{
    public static class KafkaExtensions
    {
        // Configure le producteur Kafka à partir de la configuration
        public static IServiceCollection AddKafkaProducer(this IServiceCollection services, IConfiguration config)
        {
            var producerConfig = new ProducerConfig
            {
                BootstrapServers = config["Kafka:BootstrapServers"]
            };
            services.AddSingleton(producerConfig);
            services.AddScoped<IKafkaProducer, KafkaProducer>();
            return services;
        }

        // Enregistre le service hébergé consommateur
        public static IServiceCollection AddKafkaConsumerHostedService(this IServiceCollection services, IConfiguration config)
        {
            services.AddHostedService<KafkaConsumerHostedService>();
            return services;
        }
    }
}