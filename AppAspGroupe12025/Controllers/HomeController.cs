using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppAspGroupe12025.App_Start;
using AppAspGroupe12025.Models.App_LocalResources;

namespace AppAspGroupe12025.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            this.Flash("Welcome", FlashLevel.Success);
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}