using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppAspGroupe12025.Models
{
    public class Voyage
    {
        [Key]
        public int IdVoyage { get; set; }

        [Display(Name = "Destination"), Required(ErrorMessage = "*"), MaxLength(20)]
        public string Destination { get; set; }

        [Display(Name = "DateDebut"), Required(ErrorMessage = "*")]
        public DateTime DateDebut { get; set; }

        [Display(Name = "DateRetour"), Required(ErrorMessage = "*")]
        public DateTime DateRetour { get; set; }

        
        [Display(Name = "Offre")]
        [ForeignKey("Offre")]
        public int OffreId { get; set; }
        public virtual Offre Offre { get; set; }


        [Display(Name = "Flotte")]
        [ForeignKey("Flotte")]
        public int FlotteId { get; set; }
        public virtual Flotte Flotte { get; set; }

        [Display(Name = "Chauffeur")]
        [ForeignKey("Chauffeur")]
        public int ChauffeurId { get; set; }
        public virtual Chauffeur Chauffeur { get; set; }

        public virtual ICollection<ReservationVoyage> ReservationVoyages { get; set; }
    }
}