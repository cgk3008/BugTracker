using BugTracker.Models;
using BugTracker.Models.Helper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BugTracker.Controllers
{
    public class AdminController : Controller
    {
        private ApplicationDbContext dB = new ApplicationDbContext();

        public ActionResult Index()
        {

            List<AdminIndexViewModel> model = new List<AdminIndexViewModel>();
            UserRolesHelper helper = new UserRolesHelper();

            foreach (var User in dB.Users)
            {
                AdminIndexViewModel vm = new AdminIndexViewModel();
                vm.User = User;
                vm.Roles = helper.ListRolesForUser(User.Id);
                model.Add(vm);
            }
            return View(model);
        }

        //GET: EditUser
        public ActionResult EditUser(string id)
        {
            var user = dB.Users.Find(id);
            AdminModel AdminModel = new AdminModel();
            UserRolesHelper helper = new UserRolesHelper();
            var selected = helper.ListRolesForUser(id);
            AdminModel.Roles = new MultiSelectList(dB.Roles, "Name", "Name", selected);
            AdminModel.User = new User
            {
                Id = user.Id,
                FullName = user.FullName
            };
            return View(AdminModel);

            //new { id = mod.User.Id })
        }

        //POST: EditUser
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUser(AdminModel model)
        {
            //var user = dB.Users.Find(model.id);
            UserRolesHelper helper = new UserRolesHelper();
            foreach (var rolermv in dB.Roles.Select(r => r.Name).ToList())
            {
                helper.RemoveUserFromRole(model.User.Id, rolermv);
            }
            foreach (var roleadd in model.SelectedRoles)
            {
                helper.AddUserToRole(model.User.Id, roleadd);
            }
            return RedirectToAction("Index");
        }


        //GET: IsUserInRole
        public ActionResult UsersNotInRole(string id)
        {
            var user = dB.Users.Find(id);
            AdminModel AdminModel = new AdminModel();
            UserRolesHelper helper = new UserRolesHelper();
            var selected = helper.ListUsersNotInRole(id);
            AdminModel.Roles = new MultiSelectList(dB.Roles, "Name", "Name", selected);
            AdminModel.User = new User
            {
                Id = user.Id,
                FullName = user.FullName
            };
            return View(AdminModel);

            //new { id = mod.User.Id })
        }

        //POST: IsUserInRole
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UsersNotInRole(AdminModel model)
        {
            //var user = dB.Users.Find(model.id);
            UserRolesHelper helper = new UserRolesHelper();
            foreach (var rolermv in dB.Roles.Select(r => r.Name).ToList())
            {
                helper.RemoveUserFromRole(model.User.Id, rolermv);
            }
            foreach (var roleadd in model.SelectedRoles)
            {
                helper.AddUserToRole(model.User.Id, roleadd);
            }
            return RedirectToAction("Index");
        }



    }
}





