using EasyNetQ.Topology;

namespace EasyNetQ.Customers.API.MessageBus
{
    public class EasyNetQService : IMessageBusService
    {
        private readonly IAdvancedBus _bus;
        const string EXCHANGE = "curso-rabbitmq";
        public EasyNetQService(IBus bus)
        {
            _bus = bus.Advanced;
        }

        public async Task PublishAsync<T>(string routingKey, T @event)
        {
            var exchange = await _bus.ExchangeDeclareAsync(EXCHANGE, ExchangeType.Topic);

            await _bus.PublishAsync(exchange, routingKey, true, new Message<T>(@event));
        }
    }
}
