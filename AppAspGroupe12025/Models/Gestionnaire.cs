using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace AppAspGroupe12025.Models
{
    public class Gestionnaire:Utilisateur
    {
        [Display(Name = "CNI"), Required(ErrorMessage = "*"), MaxLength(20)]
        public string CNIGestionnaire { get; set; }

        public int? IdAgence { get; set; }
        [ForeignKey("IdAgence")]

        public virtual Agence Agence { get; set; }

        //public int IdAnnonce { get; set; }
        //[ForeignKey("IdAnnonce")]

        //public virtual Annonce Annonce { get; set; }

        public virtual ICollection<Agence> Agences { get; set; }
    }
}