using System.Text;
using ms.communications.Events;
using RabbitMQ.Client;
using Microsoft.Extensions.Configuration;

namespace ms.communications.Producers
{
    public class RabbitEventProducer : IRabbitProducer
    {
        private readonly IConfiguration _configuration;
        public RabbitEventProducer(IConfiguration configuration) => _configuration = configuration;

        public void Produce(RabbitMqEvent rabbitmqEvent) 
        {

            var factory = new ConnectionFactory() {
                HostName = _configuration["Communication:EventBus:HostName"],
                UserName = _configuration["Communication:EventBus:UserName"],
                Password = _configuration["Communication:EventBus:Password"]
            };
            Console.WriteLine($"Connection:{factory.HostName},{factory.UserName},{factory.Password}");
            Console.WriteLine($"Connection:Creating");
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                Console.WriteLine($"Connection:Stablished");
                var queue = rabbitmqEvent.GetType().Name;
                Console.WriteLine($"Queue:{queue}");

                channel.QueueDeclare(queue, durable: true, exclusive: false, autoDelete: false, null);
                
                var body = Encoding.UTF8.GetBytes(rabbitmqEvent.Serialize());
                
                Console.WriteLine($"Producer:{rabbitmqEvent.Serialize()}");

                channel.BasicPublish("", queue, null, body);
                Console.WriteLine($"Producer:Sent!");
            }
        }
    }
}
