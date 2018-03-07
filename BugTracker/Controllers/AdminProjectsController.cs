using BugTracker.Models;
using BugTracker.Models.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BugTracker.Controllers
{
    public class AdminProjectsController : Controller
    {
        private ApplicationDbContext dB = new ApplicationDbContext();

        public ActionResult Index()
        {
            //Jeex look at all this code I don't need!!!!
            //List<AdminIndexViewModel> model = new List<AdminIndexViewModel>();
            //ProjectHelper helper = new ProjectHelper();

            //foreach (var Project in dB.Project)
            //{
            //    AdminIndexViewModel vm = new AdminIndexViewModel();
            //    vm.Project = Project;
            //    vm.Users = helper.ListProjectsForUser(User.Id);
            //    model.Add(vm);
            //}
            return View(dB.Project.Include("User").ToList());

            //ok need to adjust Users and User. go to project and adjust dB context reference???
        }

        //GET: EditUser
        public ActionResult EditProject(int id) 
        {
            var project = dB.Project.Find(id);
            AdminProject AdminProject = new AdminProject();
            ProjectHelper helper = new ProjectHelper();
            var selected = project.User;
            AdminProject.Users = new MultiSelectList(dB.Users, "Id", "FullName", selected);
            AdminProject.Project = project;
            return View(AdminProject);


        }

        //POST: EditUser
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProject(AdminProject model)
        {

            ProjectHelper helper = new ProjectHelper();
            foreach (var userrmv in dB.Users.Select(r => r.Id).ToList())
            {
                helper.RemoveUserFromProject(userrmv, model.Project.Id );
            }

            foreach (var useradd in model.SelectedUsers)
            {
                helper.AddUserToProject(useradd, model.Project.Id );
            }
            return RedirectToAction("Index");
        }




    }
}