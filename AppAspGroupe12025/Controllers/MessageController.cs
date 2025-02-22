using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AppAspGroupe12025.Models;

namespace AppAspGroupe12025.Controllers
{
    public class MessageController : Controller
    {
        // GET: Message
        //public ActionResult Index()
        //{
        //    return View();
        //}

        private readonly Message _message;

        public MessageController()
        {
            _message = new Message();
        }

        // Page d'accueil du formulaire
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> EnvoyerMessage(string numero, string message)
        {
            if (string.IsNullOrEmpty(numero) || string.IsNullOrEmpty(message))
            {
                return Content("Numéro ou message vide !");
            }

            string result = await _message.SendMessageAsync(numero, message);
            return Content(result);
        }
    }
}