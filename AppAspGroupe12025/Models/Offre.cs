using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AppAspGroupe12025.Models
{
    public class Offre
    {
        [Key]
        public int IdOffre { get; set; }

        [Display(Name = "Description"), Required(ErrorMessage = "*"), MaxLength(20)]
        public string Description { get; set; }

        [Display(Name = "Prix"), Required(ErrorMessage = "*")]
        public float Prix { get; set; }

        [Display(Name = "Disponibilité"), Required(ErrorMessage = "*")]
        public bool Disponibilité { get; set; }

        public virtual ICollection<Voyage> Voyages { get; set; }
    }
}