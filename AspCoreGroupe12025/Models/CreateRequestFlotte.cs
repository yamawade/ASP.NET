using System.ComponentModel.DataAnnotations;

namespace AspCoreGroupe12025.Models
{
    public class CreateRequestFlotte
    {
        [Required(ErrorMessage = "*"), MaxLength(80)]
        public string TypeFlotte { get; set; }
        [Required(ErrorMessage = "*"), MaxLength(80)]
        public string MatriculeFlotte { get; set; }

    }
}
