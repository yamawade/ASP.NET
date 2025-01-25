using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AppAspGroupe12025.Models
{
    public class Reservation
    {
        [Key]
        public int IdReservation{ get; set; }

        [Display(Name = "Date"), Required(ErrorMessage = "*")]
        public DateTime Date { get; set; }

        [Display(Name = "Statut"), Required(ErrorMessage = "*"), MaxLength(20)]
        public string Statut { get; set; }

        public virtual ICollection<ReservationVoyage> ReservationVoyages { get; set; }
    }
}