//using RabbitMQ.Client;
//using RabbitMQ.Client.Events;
//using System.Text;

//namespace AspCoreGroupe12025.Services
//{
//    public class RabbitMQConsumer : BackgroundService
//    {
//        private IConnection _connection;
//        private IModel _channel;

//        public RabbitMQConsumer()
//        {
//            var factory = new ConnectionFactory() { HostName = "localhost" };

//            _connection = factory.CreateConnection();
//            _channel = _connection.CreateModel();

//            _channel.QueueDeclare(queue: "demo-queue",
//                                 durable: false,
//                                 exclusive: false,
//                                 autoDelete: false,
//                                 arguments: null);
//        }

//        protected override Task ExecuteAsync(CancellationToken stoppingToken)
//        {
//            var consumer = new EventingBasicConsumer(_channel);

//            consumer.Received += (model, ea) =>
//            {
//                var body = ea.Body.ToArray();
//                var message = Encoding.UTF8.GetString(body);
//                Console.WriteLine($" Message reçu : {message}");
//            };

//            _channel.BasicConsume(queue: "demo-queue",
//                                 autoAck: true,
//                                 consumer: consumer);

//            return Task.CompletedTask;
//        }

//        public override void Dispose()
//        {
//            _channel.Close();
//            _connection.Close();
//            base.Dispose();
//        }
//    }
//}
