using AspCoreGroupe12025.Entities;
using AspCoreGroupe12025.Models;
using AspCoreGroupe12025.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AspCoreGroupe12025.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgencesController : ControllerBase
    {
        private readonly IAgenceService _agenceService;
        private readonly IMapper _mapper;

        public AgencesController(IAgenceService agenceService, IMapper mapper)
        {
            _agenceService = agenceService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var agences = _agenceService.GetAll();
            return Ok(agences);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var agence = _agenceService.GetById(id);
            if (agence == null)
                return NotFound();
            return Ok(agence);
        }

        [HttpPost]
        public IActionResult Create(CreateAgenceRequest model)
        {
            _agenceService.Create(model);
            return Ok(new { message = "Agence créée avec succès" });
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateAgenceRequest model)
        {
            _agenceService.Update(id, model);
            return Ok(new { message = "Agence mise à jour avec succès" });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _agenceService.Delete(id);
            return Ok(new { message = "Agence supprimée avec succès" });
        }
    }
}
