using ms.communications.Consumers;
using ms.communications.Events;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace API.WorkTimeRecord.Consumers
{
    public class RabbitMqEntityConsumer: IRabbitMqConsumer
    {
       
        private IConnection _connection;
        private readonly IConfiguration _configuration;
        public RabbitMqEntityConsumer(IConfiguration configuration)
        {
            
            _configuration = configuration;
        }

        public void Unsubscribe() => _connection?.Dispose();

        public void Subscribe() {
            var factory = new ConnectionFactory()
            {
                HostName = _configuration["Communication:EventBus:HostName"],
                UserName = _configuration["Communication:EventBus:UserName"],
                Password = _configuration["Communication:EventBus:Password"]
            };
            Console.WriteLine($"Connection:{factory.HostName},{factory.UserName},{factory.Password}");            
            Console.WriteLine("Connection:Creating");
            _connection = factory.CreateConnection();
            Console.WriteLine("Connection:Created");
            var channel = _connection.CreateModel();

            var queue = typeof(EntityCreatedEvent).Name;
            Console.WriteLine($"Queue:{queue}");
            channel.QueueDeclare(queue, durable: true, exclusive: false, autoDelete: false, null);
            Console.WriteLine("Consumer:Listening...");
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += ReceivedEvent;

            channel.BasicConsume(queue: queue, autoAck: true, consumer: consumer);
        }
        private async void ReceivedEvent(object sender, BasicDeliverEventArgs e) {
            Console.WriteLine("Consumer:Consuming Message");
            if (e.RoutingKey == typeof(EntityCreatedEvent).Name)
            {
                
                var message = Encoding.UTF8.GetString(e.Body.Span);
                Console.WriteLine("Message Received");
                Console.WriteLine(message);

                var employeeCreatedEvent = JsonSerializer.Deserialize<EntityCreatedEvent>(message);
                
                //await _mongorepository.CreateMongoEntity(new Entities.MongoRecord() { Value = "CRUEL" });
            }
        }   
    }
}
