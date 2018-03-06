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
    public class AdminUserViewsController : Controller
    {
        public ActionResult Index()
        {
            //private UserManager<User> userManager = new UserManager<User>(new UserStore<User>(new ApplicationDbContext()));
        private ApplicationDbContext dB = new ApplicationDbContext();

        List<AdminIndexViewModel> model = new List<AdminIndexViewModel>();
            UserRolesHelper helper = new UserRolesHelper();
            foreach (var User in dB.Users)
            {
                AdminIndexViewModel vm = new AdminIndexViewModel()
                    vm.User = user;
                vm.Roles = helper.ListUserRoles(user.Id);
                model.Add(vm);
            }
            return View(db.Users.ToList());
        }

        public ActionResult EditUser(string id)
        {
            var user = dB.Users.Find(id);
            AdminUserViewModel AdminModel = new AdminUserViewModel();
            UserRolesHelper helper = new UserRolesHelper();
            var selected = helper.ListUserRoles(id);
            AdminModel.Roles = new MultiSelectList(dB.Roles, "Name",          )
            AdminModel.User.Id = user.Id;
            AdminModel.User.FullName = user.FullName;
            return View(AdminModel);

        }


        //POST: EditUser
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUser(AdminUserViewModel model)
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
            return RediretToAction("Index");
        }





    }
}





