namespace EasyNetQ.Customers.API.MessageBus
{
    public interface IMessageBusService
    {
        Task PublishAsync<T>(string routingKey, T @event);
    }
}
