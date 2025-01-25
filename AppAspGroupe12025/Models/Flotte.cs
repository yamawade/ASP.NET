using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AppAspGroupe12025.Models
{
    public class Flotte
    {
        [Key]
        public int IdFlotte { get; set; }

        [Display(Name = "Type"), Required(ErrorMessage = "*"), MaxLength(20)]
        public string Type { get; set; }

        [Display(Name = "Matricule"), Required(ErrorMessage = "*"), MaxLength(20)]
        public string MatriculeFlotte { get; set; }

        public virtual ICollection<Voyage> Voyages { get; set; }
    }
}