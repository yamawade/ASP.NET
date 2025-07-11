using AspCoreGroupe12025.Entities;
using AspCoreGroupe12025.Helpers;
using AspCoreGroupe12025.Models;
using AspCoreGroupe12025.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AspCoreGroupe12025.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlotteController : ControllerBase
    {
        private readonly IFlotteService _FlotteService;
        private readonly IMapper _mapper;
        private readonly ILogger<FlotteController> _logger;

        public FlotteController(
            IFlotteService flotteService,
            IMapper mapper, ILogger<FlotteController> logger)
        {
            _FlotteService = flotteService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                _logger.LogInformation(" Récupération de toutes les flottes en cours...");
                var flottes = _FlotteService.GetAll();
                return Ok(flottes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " Une erreur s'est produite lors de la récupération des flottes.");
                return StatusCode(500, new { message = "An error occurred while processing your request." });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var flotte = _FlotteService.GetById(id);
                if (flotte == null)
                {
                    _logger.LogWarning(" Flotte avec l'identifiant {Id} non trouvée.", id);
                    return NotFound(new { message = "Flotte not found." });
                }

                return Ok(flotte);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " Une erreur s'est produite lors de la récupération de la flotte avec l'identifiant {Id}.", id);
                return StatusCode(500, new { message = "An error occurred while processing your request." });
            }
        }

        [HttpPost]
        public IActionResult Create(CreateRequestFlotte model)
        {
            try
            {
                _FlotteService.Create(model);
                _logger.LogInformation(" Nouvelle flotte créée avec succès.");
                return Ok(new { message = "Flotte created" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " Une erreur s'est produite lors de la création d'une flotte.");
                return StatusCode(500, new { message = "An error occurred while creating the flotte." });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateRequestFlotte model)
        {
            try
            {
                _FlotteService.Update(id, model);
                _logger.LogInformation(" Flotte avec l'identifiant {Id} mise à jour avec succès.", id);
                return Ok(new { message = "Flotte updated" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " Une erreur s'est produite lors de la mise à jour de la flotte avec l'identifiant {Id}.", id);
                return StatusCode(500, new { message = "An error occurred while updating the flotte." });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _FlotteService.Delete(id);
                _logger.LogInformation(" Flotte avec l'identifiant {Id} supprimée avec succès.", id);
                return Ok(new { message = "Flotte deleted" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " Une erreur s'est produite lors de la suppression de la flotte avec l'identifiant {Id}.", id);
                return StatusCode(500, new { message = "An error occurred while deleting the flotte." });
            }
        }
    }

}
