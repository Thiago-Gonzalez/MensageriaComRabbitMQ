using RabbitMQ.Client;

namespace RabbitMQClient.Customers.API.Interfaces
{
    public interface IRabbitMQConnection : IAsyncDisposable
    {
        bool IsConnected { get; }
        ValueTask<IChannel> CreateChannelAsync();
    }
}
