using BugTracker.Models;
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

            return View(dB.Users.GetEnumerator("Project").ToList());







            //helper method 
            //public ICollection<Project> ListProjectsForUser(string userId)
            //{

            //    return dB.Users.Find(userId).Project.ToList();




                //return View(dB.Project.Include("User").ToList());

                //ok need to adjust Users to User. go to project and adjust dB context reference???
            }



    }
}