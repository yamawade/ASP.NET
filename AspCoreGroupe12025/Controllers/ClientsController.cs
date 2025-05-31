using AspCoreGroupe12025.Models;
using AspCoreGroupe12025.Services;
using Microsoft.AspNetCore.Mvc;

namespace AspCoreGroupe12025.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientsController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var clients = _clientService.GetAll();
            return Ok(clients);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var client = _clientService.GetById(id);
            return Ok(client);
        }

        [HttpPost]
        public IActionResult Create(CreateClientRequest model)
        {
            _clientService.Create(model);
            return Ok(new { message = "Client created" });
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateClientRequest model)
        {
            _clientService.Update(id, model);
            return Ok(new { message = "Client updated" });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _clientService.Delete(id);
            return Ok(new { message = "Client deleted" });
        }
    }
}
