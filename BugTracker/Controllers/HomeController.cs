using BugTracker.Models;
using BugTracker.Models.Helper;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BugTracker.Controllers
{
    //[RequireHttps]
    public class HomeController : Controller
    {
        private ApplicationDbContext dB = new ApplicationDbContext();

        [Authorize]
        public ActionResult Index()
        {
            
            var userId = User.Identity.GetUserId();
            //var user = dB.Users.Find(userId);
            var userProjects = dB.Project.Where(p => p.Users.Any(u => u.Id == userId)).ToList();
            var userTickets = dB.Ticket.Where(t => t.AssignedToUserId == userId || t.OwnerUserId == userId /*|| t.PmId == userId*/).ToList();
            var userNotifications = dB.Notification.Where(n => n.UserId == userId).ToList();

            DashboardViewModel model = new DashboardViewModel()
            {
                Projects = userProjects,
                TicketNotifications = userNotifications,
                Tickets = userTickets
            };

            return View(model);           
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