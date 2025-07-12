using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using AspCoreGroupe12025.Events;
using AspCoreGroupe12025.Entities;
using Microsoft.Extensions.Logging;

namespace AspCoreGroupe12025.HostedServices
{
    public class KafkaConsumerHostedService : BackgroundService
    {
        private readonly IConsumer<Null, string> _consumer;
        private readonly IConfiguration _config;
        private readonly ILogger<KafkaConsumerHostedService> _logger;

        public KafkaConsumerHostedService(IConfiguration config, ILogger<KafkaConsumerHostedService> logger)
        {
            _config = config;
            _logger = logger;
            var consumerConfig = new ConsumerConfig
            {
                BootstrapServers = _config["Kafka:BootstrapServers"],
                GroupId = _config["Kafka:GroupId"],
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
            _consumer = new ConsumerBuilder<Null, string>(consumerConfig).Build();
            _consumer.Subscribe(new[]
            {
                _config["Kafka:UserEventsTopic"],
                _config["Kafka:FlotteEventsTopic"]
            });
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var result = _consumer.Consume(stoppingToken);
                    // Désérialisation conditionnelle selon le topic
                    if (result.Topic == _config["Kafka:UserEventsTopic"])
                    {
                        var evt = JsonSerializer.Deserialize<UserEvent>(result.Message.Value);
                        _logger.LogInformation("Événement utilisateur reçu : {Event}", evt);
                    }
                    else
                    {
                        var evt = JsonSerializer.Deserialize<FlotteEvent>(result.Message.Value);
                        _logger.LogInformation("Événement flotte reçu : {Event}", evt);
                    }
                }
                catch (OperationCanceledException) { }
            }
        }

        public override void Dispose()
        {
            _consumer.Close();
            _consumer.Dispose();
            base.Dispose();
        }
    }
}