using BugTracker.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BugTracker.Controllers
{
    public class AssignedTicketsController : Controller
    {

        private ApplicationDbContext dB = new ApplicationDbContext();

        // GET: AssignedTickets
        public ActionResult Index()
        {

            var userId = User.Identity.GetUserId();

            return View(dB.Users.Find(userId).Ticket.ToList());
        }
    }
}