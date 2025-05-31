using System.ComponentModel.DataAnnotations;

namespace AspCoreGroupe12025.Models
{
    public class CreateClientRequest
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public string Adresse { get; set; }

        [Phone]
        public string Telephone { get; set; }

        [Required]
        public string CNIClient { get; set; }
    }
}
