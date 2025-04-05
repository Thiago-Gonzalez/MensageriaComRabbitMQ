
namespace MassTransit.Customers.API.MessageBus
{
    public class MessageBusService : IMessageBusService
    {
        private readonly IBus _bus;
        public MessageBusService(IBus bus)
        {
            _bus = bus;
        }

        public async Task PublishAsync<T>(T message)
        {
            await _bus.Publish(message);
        }
    }
}
