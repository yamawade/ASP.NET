using System.ComponentModel.DataAnnotations;
namespace AspCoreGroupe12025.Entities
{
    public class Flotte
    {
        [Key]
        public int IdFlotte { get; set; }
        [Display(Name = "Type"), Required(ErrorMessage = "*"), MaxLength(80)]
        public string TypeFlotte { get; set; }
        [Display(Name = "Matricule"), Required(ErrorMessage = "*"), MaxLength(80)]
        public string MatriculeFlotte { get; set; }
    }

}
