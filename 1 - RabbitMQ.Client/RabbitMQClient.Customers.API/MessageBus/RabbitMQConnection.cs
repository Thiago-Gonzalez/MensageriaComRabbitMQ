using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQClient.Customers.API.MessageBus.Configuration;
using RabbitMQClient.Customers.API.Interfaces;

namespace RabbitMQClient.Customers.API.Bus
{
    public class RabbitMQConnection : IRabbitMQConnection
    {
        private readonly IConnection _connection;
        public RabbitMQConnection(IOptions<RabbitMQSettings> options)
        {
            try
            {
                var connectionFactory = new ConnectionFactory
                {
                    HostName = options.Value.Host,
                    Port = options.Value.Port,
                    UserName = options.Value.User,
                    Password = options.Value.Password,
                    VirtualHost = options.Value.VirtualHost
                };

                _connection = connectionFactory.CreateConnectionAsync("curso-rabbitmq-client-publisher-subscribe").GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to establish RabbitMQ connection. Error: {ex.Message}");
            }
        }

        public bool IsConnected => _connection?.IsOpen ?? false;

        public async ValueTask<IChannel> CreateChannelAsync()
        {
            if (!IsConnected)
                throw new InvalidOperationException("Cannot create channel. RabbitMQ connection is closed.");

            return await _connection.CreateChannelAsync();
        }

        public async ValueTask DisposeAsync()
        {
            if (IsConnected)
                await _connection.CloseAsync();

            await _connection.DisposeAsync();
        }
    }
}
