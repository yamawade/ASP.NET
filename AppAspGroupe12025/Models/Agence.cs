using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppAspGroupe12025.Models
{
    public class Agence
    {
        [Key]

        public int IdAgence { get; set; }

        [Display(Name = "Ninea"), Required(ErrorMessage = "*"), MaxLength(20)]
        public string NineaAgence { get; set; }

        [Display(Name = "Adresse"), Required(ErrorMessage = "*"), MaxLength(150)]
        public string AdresseAgence { get; set; }
        [Display(Name = "Longitude")]
        public float? Longitude { get; set; }
        [Display(Name = "Latitude")]
        public float? Latitude { get; set; }

        [Display(Name = "RCCM"), Required(ErrorMessage = "*"), MaxLength(20)]
        public string RccmAgence { get; set; }

        public int? IdGestionnaire { get; set; }
        [ForeignKey("IdGestionnaire")]

        public virtual Gestionnaire Gestionnaire { get; set; }

        public virtual ICollection<Offre> Offres { get; set; }
    }
}