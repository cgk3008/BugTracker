using BugTracker.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

          

            //return View(dB.Users.Find(userId).Ticket.ToList());


            //example from assignedProjects controller return View(dB.Users.Find(userId).Project.ToList()); can't figure out why above does not work, need to look at ticket model more compared to project model

            var tickets = dB.Ticket.Where(u => u.AssignedToUserId == userId).Include(t => t.AssignedToUser).Include(t => t.OwnerUser).Include(t => t.Project).Include(t => t.Priority).Include(t => t.Status).Include(t => t.Type);
            return View(tickets.ToList());



        }
    }
}


