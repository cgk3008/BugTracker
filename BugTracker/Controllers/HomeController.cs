using BugTracker.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BugTracker.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            EmailModel model = new EmailModel();
            return View(model);
        }

        private const string key = "qazqaz1288"; //You can add your own Key

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult DemoLogin()
        {
            return RedirectToAction("Index", "Home");
        }

        public ActionResult LP()
        {
            return View();
        }


    }
}