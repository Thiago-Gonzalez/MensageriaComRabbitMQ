namespace MassTransit.Customers.API.MessageBus
{
    public interface IMessageBusService
    {
        Task PublishAsync<T>(T message);
    }
}
