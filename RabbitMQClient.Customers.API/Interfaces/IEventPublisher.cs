namespace RabbitMQClient.Customers.API.Interfaces
{
    public interface IEventPublisher<T>
    {
        Task PublishAsync(T @event);
    }
}
