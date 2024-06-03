using ms.communications.Events;

namespace ms.communications.Producers
{
    public interface IRabbitProducer {
        void Produce(RabbitMqEvent rabbitmqEvent);
    }
}