using EasyNetQ.Customers.API.MessageBus;
using MessagingEvents.Shared;
using MessagingEvents.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace EasyNetQ.Customers.API.Controllers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IMessageBusService _messageBusService;
        const string ROUTING_KEY = "customer-created";

        public CustomersController(IMessageBusService messageBusService)
        {
            _messageBusService = messageBusService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CustomerInputModel model)
        {
            var @event = new CustomerCreated(model.FullName, model.Email, model.PhoneNumber, model.BirthDate);

            await _messageBusService.PublishAsync(ROUTING_KEY, @event);

            return NoContent();
        }
    }
}
