using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AspCoreGroupe12025.Entities
{
    public class Voyage
    {
        [Key]
        public int IdVoyage { get; set; }

        [Display(Name = "Destination"), Required(ErrorMessage = "*"), MaxLength(20)]
        public string Destination { get; set; }

        [Display(Name = "DateDebut"), Required(ErrorMessage = "*")]
        public DateTime DateDebut { get; set; }

        [Display(Name = "DateRetour"), Required(ErrorMessage = "*")]
        public DateTime DateRetour { get; set; }


      
    }
}
