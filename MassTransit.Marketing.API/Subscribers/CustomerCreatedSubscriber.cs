using MessagingEvents.Shared;
using MessagingEvents.Shared.Services;

namespace MassTransit.Marketing.API.Subscribers
{
    public class CustomerCreatedSubscriber : IConsumer<CustomerCreated>
    {
        public IServiceProvider ServiceProvider { get; }
        public CustomerCreatedSubscriber(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        public async Task Consume(ConsumeContext<CustomerCreated> context)
        {
            var @event = context.Message;

            using (var scope = ServiceProvider.CreateScope()) 
            { 
                var service = scope.ServiceProvider.GetRequiredService<INotificationService>();

                await service.SendEmail(@event.Email, "boas-vindas", new Dictionary<string, string> { { "name", @event.FullName } });
            }
        }
    }
}
