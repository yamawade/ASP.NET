using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspCoreGroupe12025.Controllers
{
    public class ProductsController : ControllerBase
    {
        [HttpGet]
        [Authorize] // Nécessite un token JWT valide
        public IActionResult GetProduits()
        {
            return Ok(new { message = "Produit récupéré avec succès !" });
        }
    }
}
