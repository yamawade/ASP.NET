using System.Data;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace AspCoreGroupe12025.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(20)]
        public string Title { get; set; }

        [Required, MaxLength(80)]
        public string FirstName { get; set; }

        [Required, MaxLength(80)]
        public string LastName { get; set; }

        [Required, MaxLength(80),DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public Role Role { get; set; }

        [JsonIgnore, MaxLength(255)]
        public string PasswordHash { get; set; }
    }
}
