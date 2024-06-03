namespace ms.communications.Consumers
{
    public interface IRabbitMqConsumer
    {
        void Subscribe();
        void Unsubscribe();
    }
}
