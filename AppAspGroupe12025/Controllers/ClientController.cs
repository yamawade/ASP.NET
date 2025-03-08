using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppAspGroupe12025.Models;

namespace AppAspGroupe12025.Controllers
{
    public class ClientController : Controller
    {
        BDAgenceVoyageContext db = new BDAgenceVoyageContext();
        // GET: Client
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult List()
        {
            return Json(db.clients.ToList(), JsonRequestBehavior.AllowGet);
        }

        GMailer gmailer = new GMailer();

        public JsonResult Add(Client client)
        {
            db.clients.Add(client);
            db.SaveChanges();
            gmailer.SendEmail(client.EmailUtilisateur, "Inscription avec success !",$"Bonjour {client.PrenomUtilisateur + " " + client.NomUtilisateur}, votre inscription a ete bien enregistrer");
            return Json(1, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetbyID(int ID)
        {
            var Client = db.clients.ToList().Find(x => x.IdUtilisateur.Equals(ID));
            return Json(Client, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Update(Client client) 
        {
            Client e = db.clients.Find(client.IdUtilisateur);
            e.CNIClient = client.CNIClient;
            e.NomUtilisateur = client.NomUtilisateur;
            e.PrenomUtilisateur = client.PrenomUtilisateur;
            e.TelUtilisateur = client.TelUtilisateur;
            e.EmailUtilisateur = client.EmailUtilisateur;
            db.SaveChanges();
            return Json(1, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Delete(int ID)
        {
            Client e = db.clients.Find(ID);
            db.clients.Remove(e);
            db.SaveChanges();
            return Json(0, JsonRequestBehavior.AllowGet);
        }
    }
} 