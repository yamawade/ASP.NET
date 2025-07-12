using System;
using System.Threading.Tasks;
using AspCoreGroupe12025.Entities;
using AspCoreGroupe12025.Events;
using AspCoreGroupe12025.Models;
using AspCoreGroupe12025.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AspCoreGroupe12025.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlotteController : ControllerBase
    {
        private readonly IFlotteService _flotteService;
        private readonly IKafkaProducer _producer;
        private readonly ILogger<FlotteController> _logger;
        private readonly IConfiguration _configuration;

        public FlotteController(
            IFlotteService flotteService,
            IKafkaProducer producer,
            ILogger<FlotteController> logger,
            IConfiguration configuration)
        {
            _flotteService = flotteService;
            _producer = producer;
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                _logger.LogInformation("Récupération de toutes les flottes...");
                var flottes = _flotteService.GetAll();
                return Ok(flottes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la récupération des flottes.");
                return StatusCode(500, new { message = "Une erreur est survenue." });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var flotte = _flotteService.GetById(id);
                if (flotte == null)
                {
                    _logger.LogWarning("Flotte {Id} non trouvée.", id);
                    return NotFound(new { message = "Flotte non trouvée." });
                }
                return Ok(flotte);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la récupération de la flotte {Id}.", id);
                return StatusCode(500, new { message = "Une erreur est survenue." });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRequestFlotte model)
        {
            try
            {
                // 1) Création en base
                var created = _flotteService.Create(model);
                _logger.LogInformation("Nouvelle flotte créée (Id={Id}).", created.IdFlotte);

                // 2) Récupération sûre du topic
                var topic = _configuration["Kafka:FlotteEventsTopic"]
                            ?? throw new InvalidOperationException("Topic Kafka non configuré.");

                // 3) Construction et publication de l'événement
                // Adapte 'TypeFlotte' si ta Flotte expose une autre prop.
                var evt = new FlotteEvent(
                    created.IdFlotte,
                    created.TypeFlotte,
                    created.MatriculeFlotte, 
                    "Created",
                    DateTime.UtcNow
                );
                await _producer.ProduceAsync(topic, evt);

                return Ok(new { message = "Flotte créée.", id = created.IdFlotte });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la création de la flotte.");
                return StatusCode(500, new { message = "Une erreur est survenue." });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateRequestFlotte model)
        {
            try
            {
                _flotteService.Update(id, model);
                _logger.LogInformation("Flotte {Id} mise à jour.", id);

                // Publication de l'événement Kafka
                var evt = new FlotteEvent(
                    id,
                    model.TypeFlotte,
                    model.MatriculeFlotte,
                    "Updated",
                    DateTime.UtcNow
                );

                await _producer.ProduceAsync("flotte-events", evt);

                return Ok(new { message = "Flotte mise à jour." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la mise à jour de la flotte {Id}.", id);
                return StatusCode(500, new { message = "Une erreur est survenue." });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                _flotteService.Delete(id);
                _logger.LogInformation("Flotte {Id} supprimée.", id);

                // Publication de l'événement Kafka
                var evt = new FlotteEvent(
                    id,
                    TypeFlotte: null,
                    MatriculeFlotte: null,
                    EventType: "Deleted",
                    Timestamp: DateTime.UtcNow
                );

                await _producer.ProduceAsync("flotte-events", evt);

                return Ok(new { message = "Flotte supprimée." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la suppression de la flotte {Id}.", id);
                return StatusCode(500, new { message = "Une erreur est survenue." });
            }
        }
    }
}
