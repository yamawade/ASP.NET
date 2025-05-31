using System.ComponentModel.DataAnnotations;

namespace AspCoreGroupe12025.Entities
{
    public class Client : User
    {
        [MaxLength(100)]
        public string Adresse { get; set; }

        [Phone]
        public string Telephone { get; set; }
        //RegularExpression("/^(1\\d|2\\d)\\d{11}$/")
        [Display(Name = "CNI"), Required(ErrorMessage = "*"), MaxLength(20)]
        public string CNIClient { get; set; }
    }
}
