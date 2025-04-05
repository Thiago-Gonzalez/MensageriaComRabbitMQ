using MessagingEvents.Shared;
using RabbitMQ.Client;
using RabbitMQClient.Customers.API.Interfaces;
using System.Text;
using System.Text.Json;

namespace RabbitMQClient.Customers.API.MessageBus.Publishers
{
    public class CustomerCreatedPublisher : IEventPublisher<CustomerCreated>
    {
        private readonly IRabbitMQConnection _connection;

        const string EXCHANGE = "curso-rabbitmq";
        const string QUEUE = "customer-created";
        const string ROUTING_KEY = "customer-created";

        public CustomerCreatedPublisher(IRabbitMQConnection connection)
        {
            _connection = connection;
        }

        public async Task PublishAsync(CustomerCreated @event)
        {
            await using var channel = await _connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(QUEUE, true, false, false);

            await channel.QueueBindAsync(QUEUE, EXCHANGE, ROUTING_KEY);

            var json = JsonSerializer.Serialize(@event);

            var body = Encoding.UTF8.GetBytes(json);

            await channel.BasicPublishAsync(EXCHANGE, ROUTING_KEY, true, body);
        }
    }
}
