using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AppAspGroupe12025.Models
{
    public class Utilisateur
    {
        [Key]
        public int IdUtilisateur { get; set; }

        [Display(Name ="Nom"), Required(ErrorMessage ="*"), MaxLength(80)]
        public string NomUtilisateur { get; set; }

        [Display(Name = "Prénom"), Required(ErrorMessage = "*"), MaxLength(80)]
        public string PrenomUtilisateur { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email"), Required(ErrorMessage = "*"), MaxLength(80)]
        public string EmailUtilisateur { get; set; }


        [Display(Name = "Téléphone"), Required(ErrorMessage = "*"), MaxLength(20),RegularExpression("/ ^(77 | 76 | 75 | 78 | 70 | 33)[0 - 9]{7}$/)")]
        public string TelUtilisateur { get; set; }
    }
}