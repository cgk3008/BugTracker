using BugTracker.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BugTracker.Controllers
{
    public class AssignedProjectsController : Controller
    {
        private ApplicationDbContext dB = new ApplicationDbContext();

        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            //helper.ListProjectForUser(userId);

            return View(dB.Users.Find(userId).Project.ToList());

            //return View(dB.Users.GetEnumerator("Project").ToList());

            //return View();

            //helper method 
            //public ICollection<Project> ListProjectsForUser(string userId)
            //{

            //    return dB.Users.Find(userId).Project.ToList();

        }
    }
}