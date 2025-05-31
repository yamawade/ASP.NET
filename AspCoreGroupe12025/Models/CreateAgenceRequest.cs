using System.ComponentModel.DataAnnotations;

namespace AspCoreGroupe12025.Models
{
    public class CreateAgenceRequest
    {
        [Required, MaxLength(20)]
        public string NineaAgence { get; set; }

        [Required, MaxLength(150)]
        public string AdresseAgence { get; set; }

        public float? Longitude { get; set; }
        public float? Latitude { get; set; }

        [Required, MaxLength(20)]
        public string RccmAgence { get; set; }

        public int? IdGestionnaire { get; set; }
    }

    // UpdateAgenceRequest.cs
    public class UpdateAgenceRequest : CreateAgenceRequest
    {
        // On peut réutiliser la même structure que Create
    }
}
