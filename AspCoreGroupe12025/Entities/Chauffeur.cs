using System.ComponentModel.DataAnnotations;

namespace AspCoreGroupe12025.Entities
{
    public class Chauffeur: User
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(80)]
        public string Nom { get; set; }
        [Required, MaxLength(80)]
        public string Prenom { get; set; }

        public virtual ICollection<Voyage> Voyages { get; set; }
    }
}
