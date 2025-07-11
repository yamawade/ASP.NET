using System.ComponentModel.DataAnnotations;

namespace AspCoreGroupe12025.Models
{
    public class UpdateRequestFlotte
    {
        [Required(ErrorMessage = "*"), MaxLength(80)]
        public string TypeFlotte { get; set; }
        [Required(ErrorMessage = "*"), MaxLength(80)]
        public string MatriculeFlotte { get; set; }

        private string replaceEmptyWithNull(string value)
        {
            // replace empty string with null to make field optional 
            return string.IsNullOrEmpty(value) ? null : value;
        }

    }
}
