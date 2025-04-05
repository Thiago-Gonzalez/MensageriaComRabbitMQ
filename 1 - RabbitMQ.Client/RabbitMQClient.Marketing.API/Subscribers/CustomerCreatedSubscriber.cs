using MessagingEvents.Shared;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace RabbitMQClient.Marketing.API.Subscribers
{
    public class CustomerCreatedSubscriber : IHostedService
    {
        private readonly IChannel _channel;
        const string CUSTOMER_CREATED_QUEUE = "customer-created";
        public CustomerCreatedSubscriber()
        {
            var connectionFactory = new ConnectionFactory
            {
                HostName = "localhost",
            };

            var connection = connectionFactory.CreateConnectionAsync("curso-rabbitmq-client-consumer").GetAwaiter().GetResult();

            _channel = connection.CreateChannelAsync().GetAwaiter().GetResult();
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var consumer = new AsyncEventingBasicConsumer(_channel);

            consumer.ReceivedAsync += async (sender, eventArgs) =>
            {
                var contentArray = eventArgs.Body.ToArray();
                var contentString = Encoding.UTF8.GetString(contentArray);

                var @event = JsonSerializer.Deserialize<CustomerCreated>(contentString);

                Console.WriteLine($"Message received: {contentString}");

                await _channel.BasicAckAsync(eventArgs.DeliveryTag, false);
            };

            await _channel.BasicConsumeAsync(CUSTOMER_CREATED_QUEUE, false, consumer);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
