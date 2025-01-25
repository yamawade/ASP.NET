using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppAspGroupe12025.Models
{
    public class ReservationVoyage
    {
        [Key]
        [Column(Order = 0)]
        public int VoyageId { get; set; }

        [Key]
        [Column(Order = 1)]
        public int ReservationId { get; set; }

        public virtual Voyage Voyage { get; set; }
        public virtual Reservation Reservation { get; set; }
    }
}