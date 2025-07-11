using Microsoft.AspNetCore.Mvc;
using AspCoreGroupe12025.Services;

namespace AspCoreGroupe12025.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly RabbitMQProducer _producer;

        public MessageController(RabbitMQProducer producer)
        {
            _producer = producer;
        }

        [HttpPost]
        public IActionResult SendMessage([FromBody] string message)
        {
            _producer.SendMessage(message);
            return Ok("Message envoyé à RabbitMQ !");
        }
    }
}
