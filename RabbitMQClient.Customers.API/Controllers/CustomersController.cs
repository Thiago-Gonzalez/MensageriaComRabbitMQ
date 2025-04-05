using MessagingEvents.Shared;
using MessagingEvents.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using RabbitMQClient.Customers.API.Interfaces;

namespace RabbitMQClient.Customers.API.Controllers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IEventPublisher<CustomerCreated> _publisher;

        public CustomersController(IEventPublisher<CustomerCreated> publisher)
        {
            _publisher = publisher;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CustomerInputModel model)
        {
            var @event = new CustomerCreated(model.FullName, model.Email, model.PhoneNumber, model.BirthDate);

            await _publisher.PublishAsync(@event);

            return NoContent();
        }
    }
}
