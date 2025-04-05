using MassTransit.Customers.API.MessageBus;
using MessagingEvents.Shared;
using MessagingEvents.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace MassTransit.Customers.API.Controllers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IMessageBusService _messageBusService;
        public CustomersController(IMessageBusService messageBusService)
        {
            _messageBusService = messageBusService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(CustomerInputModel model)
        {
            var @event = new CustomerCreated(model.FullName, model.Email, model.PhoneNumber, model.BirthDate);

            await _messageBusService.PublishAsync(@event);

            return NoContent();
        }
    }
}
