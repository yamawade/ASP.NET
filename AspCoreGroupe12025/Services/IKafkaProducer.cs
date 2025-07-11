namespace AspCoreGroupe12025.Services
{
    public interface IKafkaProducer
    {
        // Envoie un message sérialisé JSON vers le topic spécifié
        Task ProduceAsync<T>(string topic, T message);
    }
}
