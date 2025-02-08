using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace AppAspGroupe12025.Models
{
    public class BDAgenceVoyageContext : DbContext
    {
        public BDAgenceVoyageContext():base("ConnAgenceVoyage")
        { 
        
        }

        public DbSet<Chauffeur> Chauffeurs { get; set; }
        public DbSet<Utilisateur> utilisateurs { get; set; }
        public DbSet<Gestionnaire> gestionnaires { get; set; }
        public DbSet<Admin> admin { get; set; }
        public DbSet<Client> clients { get; set; }

        public System.Data.Entity.DbSet<AppAspGroupe12025.Models.Agence> Agences { get; set; }

        public System.Data.Entity.DbSet<AppAspGroupe12025.Models.Annonce> Annonces { get; set; }

        public System.Data.Entity.DbSet<AppAspGroupe12025.Models.Offre> Offres { get; set; }

        public System.Data.Entity.DbSet<AppAspGroupe12025.Models.Reservation> Reservations { get; set; }

        public System.Data.Entity.DbSet<AppAspGroupe12025.Models.Flotte> Flottes { get; set; }
    }
}