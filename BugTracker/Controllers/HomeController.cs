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



        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Contact(EmailModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            var body = "<p>Email From: <bold>{0}</bold> ({ 1})</ p >< p > Message:</ p >< p >{ 2}</ p > ";
        //            var from = "BugTracker<cgk3008.ck@gmail.com>";
        //            model.Body = "This is a message from your tracker site. The name and the email of the contacting person is above.";
        //            var email = new IdentityMessage(from, ConfigurationManager.AppSettings["emailto"])
        //            {
        //                Subject = "Portfolio Contact Email",
        //                Body = string.Format(body, model.FromName, model.FromEmail, model.Body),
        //                IsBodyHtml = true
        //            };
        //            var svc = new EmailService();
        //            await svc.SendAsync(email);
        //            return View(new EmailModel());
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine(ex.Message);
        //            await Task.FromResult(0);
        //        }
        //    }
        //    return View(model);
        //}


    }
}