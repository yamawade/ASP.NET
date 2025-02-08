using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AppAspGroupe12025.Models
{
    public class Client:Utilisateur
    {
        //RegularExpression("/^(1\\d|2\\d)\\d{11}$/")
        [Display(Name = "CNI"), Required(ErrorMessage = "*"), MaxLength(20)]
        public string CNIClient { get; set; }
    }
}