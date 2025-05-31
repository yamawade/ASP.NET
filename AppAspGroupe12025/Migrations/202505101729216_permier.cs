namespace AppAspGroupe12025.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class permier : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Utilisateurs",
                c => new
                    {
                        IdUtilisateur = c.Int(nullable: false, identity: true),
                        NomUtilisateur = c.String(nullable: false, maxLength: 80),
                        PrenomUtilisateur = c.String(nullable: false, maxLength: 80),
                        EmailUtilisateur = c.String(nullable: false, maxLength: 80),
                        TelUtilisateur = c.String(nullable: false, maxLength: 20),
                        MatriculeAdmin = c.String(maxLength: 20),
                        CNIGestionnaire = c.String(maxLength: 20),
                        IdAgence = c.Int(),
                        CNIClient = c.String(maxLength: 20),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Annonce_IdAnnonce = c.Int(),
                    })
                .PrimaryKey(t => t.IdUtilisateur)
                .ForeignKey("dbo.Agences", t => t.IdAgence)
                .ForeignKey("dbo.Annonces", t => t.Annonce_IdAnnonce)
                .Index(t => t.IdAgence)
                .Index(t => t.Annonce_IdAnnonce);
            
            CreateTable(
                "dbo.Agences",
                c => new
                    {
                        IdAgence = c.Int(nullable: false, identity: true),
                        NineaAgence = c.String(nullable: false, maxLength: 20),
                        AdresseAgence = c.String(nullable: false, maxLength: 150),
                        Longitude = c.Single(),
                        Latitude = c.Single(),
                        RccmAgence = c.String(nullable: false, maxLength: 20),
                        IdGestionnaire = c.Int(),
                        Gestionnaire_IdUtilisateur = c.Int(),
                    })
                .PrimaryKey(t => t.IdAgence)
                .ForeignKey("dbo.Utilisateurs", t => t.Gestionnaire_IdUtilisateur)
                .ForeignKey("dbo.Utilisateurs", t => t.IdGestionnaire)
                .Index(t => t.IdGestionnaire)
                .Index(t => t.Gestionnaire_IdUtilisateur);
            
            CreateTable(
                "dbo.Offres",
                c => new
                    {
                        IdOffre = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 20),
                        Prix = c.Single(nullable: false),
                        Disponibilité = c.Boolean(nullable: false),
                        Agence_IdAgence = c.Int(),
                    })
                .PrimaryKey(t => t.IdOffre)
                .ForeignKey("dbo.Agences", t => t.Agence_IdAgence)
                .Index(t => t.Agence_IdAgence);
            
            CreateTable(
                "dbo.Voyages",
                c => new
                    {
                        IdVoyage = c.Int(nullable: false, identity: true),
                        Destination = c.String(nullable: false, maxLength: 20),
                        DateDebut = c.DateTime(nullable: false),
                        DateRetour = c.DateTime(nullable: false),
                        OffreId = c.Int(nullable: false),
                        FlotteId = c.Int(nullable: false),
                        ChauffeurId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdVoyage)
                .ForeignKey("dbo.Chauffeurs", t => t.ChauffeurId, cascadeDelete: true)
                .ForeignKey("dbo.Flottes", t => t.FlotteId, cascadeDelete: true)
                .ForeignKey("dbo.Offres", t => t.OffreId, cascadeDelete: true)
                .Index(t => t.OffreId)
                .Index(t => t.FlotteId)
                .Index(t => t.ChauffeurId);
            
            CreateTable(
                "dbo.Chauffeurs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nom = c.String(nullable: false, maxLength: 80),
                        Prenom = c.String(nullable: false, maxLength: 80),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Flottes",
                c => new
                    {
                        IdFlotte = c.Int(nullable: false, identity: true),
                        Type = c.String(nullable: false, maxLength: 20),
                        MatriculeFlotte = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.IdFlotte);
            
            CreateTable(
                "dbo.ReservationVoyages",
                c => new
                    {
                        VoyageId = c.Int(nullable: false),
                        ReservationId = c.Int(nullable: false),
                        Reservation_IdReservation = c.Int(),
                        Voyage_IdVoyage = c.Int(),
                    })
                .PrimaryKey(t => new { t.VoyageId, t.ReservationId })
                .ForeignKey("dbo.Reservations", t => t.Reservation_IdReservation)
                .ForeignKey("dbo.Voyages", t => t.Voyage_IdVoyage)
                .Index(t => t.Reservation_IdReservation)
                .Index(t => t.Voyage_IdVoyage);
            
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        IdReservation = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Statut = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.IdReservation);
            
            CreateTable(
                "dbo.Annonces",
                c => new
                    {
                        IdAnnonce = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 20),
                        Statut = c.String(nullable: false, maxLength: 20),
                        DateDepart = c.DateTime(nullable: false),
                        DateArrivé = c.DateTime(nullable: false),
                        Localité = c.String(nullable: false, maxLength: 20),
                        IdGestionnaire = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdAnnonce)
                .ForeignKey("dbo.Utilisateurs", t => t.IdGestionnaire, cascadeDelete: true)
                .Index(t => t.IdGestionnaire);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Utilisateurs", "Annonce_IdAnnonce", "dbo.Annonces");
            DropForeignKey("dbo.Annonces", "IdGestionnaire", "dbo.Utilisateurs");
            DropForeignKey("dbo.Offres", "Agence_IdAgence", "dbo.Agences");
            DropForeignKey("dbo.ReservationVoyages", "Voyage_IdVoyage", "dbo.Voyages");
            DropForeignKey("dbo.ReservationVoyages", "Reservation_IdReservation", "dbo.Reservations");
            DropForeignKey("dbo.Voyages", "OffreId", "dbo.Offres");
            DropForeignKey("dbo.Voyages", "FlotteId", "dbo.Flottes");
            DropForeignKey("dbo.Voyages", "ChauffeurId", "dbo.Chauffeurs");
            DropForeignKey("dbo.Agences", "IdGestionnaire", "dbo.Utilisateurs");
            DropForeignKey("dbo.Agences", "Gestionnaire_IdUtilisateur", "dbo.Utilisateurs");
            DropForeignKey("dbo.Utilisateurs", "IdAgence", "dbo.Agences");
            DropIndex("dbo.Annonces", new[] { "IdGestionnaire" });
            DropIndex("dbo.ReservationVoyages", new[] { "Voyage_IdVoyage" });
            DropIndex("dbo.ReservationVoyages", new[] { "Reservation_IdReservation" });
            DropIndex("dbo.Voyages", new[] { "ChauffeurId" });
            DropIndex("dbo.Voyages", new[] { "FlotteId" });
            DropIndex("dbo.Voyages", new[] { "OffreId" });
            DropIndex("dbo.Offres", new[] { "Agence_IdAgence" });
            DropIndex("dbo.Agences", new[] { "Gestionnaire_IdUtilisateur" });
            DropIndex("dbo.Agences", new[] { "IdGestionnaire" });
            DropIndex("dbo.Utilisateurs", new[] { "Annonce_IdAnnonce" });
            DropIndex("dbo.Utilisateurs", new[] { "IdAgence" });
            DropTable("dbo.Annonces");
            DropTable("dbo.Reservations");
            DropTable("dbo.ReservationVoyages");
            DropTable("dbo.Flottes");
            DropTable("dbo.Chauffeurs");
            DropTable("dbo.Voyages");
            DropTable("dbo.Offres");
            DropTable("dbo.Agences");
            DropTable("dbo.Utilisateurs");
        }
    }
}
