using BooksShop.Order.Infra.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BooksShop.Order.API.Controllers
{    
    [ApiController]
    [Route("api/v1/[controller]")]
    public class APIController : ControllerBase
    {
        private readonly IRabbitMQMessageConsumer _rabbitMQMessageConsumer;

        public APIController(IRabbitMQMessageConsumer rabbitMQMessageConsumer)
        {
            _rabbitMQMessageConsumer = rabbitMQMessageConsumer;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            await _rabbitMQMessageConsumer.ConsumerCheckout();
            return Ok();
        }
    }
}