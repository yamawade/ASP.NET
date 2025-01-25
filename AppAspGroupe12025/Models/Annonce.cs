using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppAspGroupe12025.Models
{
    public class Annonce
    {
        [Key]
        public int IdAnnonce { get; set; }

        [Display(Name = "Description"), Required(ErrorMessage = "*"), MaxLength(20)]
        public string Description { get; set; }

        [Display(Name = "Statut"), Required(ErrorMessage = "*"), MaxLength(20)]
        public string Statut { get; set; }

        [Display(Name = "DateDepart"), Required(ErrorMessage = "*")]
        public DateTime DateDepart { get; set; }

        [Display(Name = "DateArrivé"), Required(ErrorMessage = "*")]
        public DateTime DateArrivé { get; set; }

        [Display(Name = "Localité"), Required(ErrorMessage = "*"), MaxLength(20)]
        public string Localité { get; set; }
    }
}