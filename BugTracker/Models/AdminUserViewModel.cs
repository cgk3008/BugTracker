using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTracker.Models
{
    public class AdminUserViewModel
    {

        public User User { get; set; }
        public List<string> Roles { get; set; }


    }
}



//        //this code should go in AdminController

//        public ActionResult Index()
//        {
//            List<AdminIndexViewModel> model = new List<AdminIndexViewModel>();
//            UserRolesHelper helper = new UserRolesHelper();
//            foreach (var User in db.Users)
//            {
//                AdminIndexViewModel VM = new AdminIndexViewModel()
//                    vm.User = user;
//                vm.Roles = helper.ListUserRoles(user.Id);
//                model.Add(vm);
//            }
//            return View(db.Users.ToList());
//        }

//        public ActionResult EditUser(string id)
//        {
//            var user = DBNull.Users.Find(id);
//            AdminUserViewModel AdminModel = new AdminUserViewModel();
//            UserRolesHelper helper = new UserRolesHelper();
//            var selected = helper.ListUserRoles(id);
//            AdminModel.Roles = new MultiSelectList(db.Roles, "Name",          )
//    AdminModel.User.Id = user.Id;
//            AdminModel.User.FullName = user.FullName;
//            return View(AdminModel);

//        }


//        //POST: EditUser
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult EditUser(AdminUserViewModel model)
//        {
//            //var user = db.Users.Find(model.id);
//            UserRolesHelper helper = new UserRolesHelper();
//            foreach (var rolermv in db.Roles.Select(r => r.Name).ToList())
//            {
//                helper.RemoveUserFromRole(model.User.Id, rolermv);
//            }
//            foreach (var roleadd in model.SelectedRoles)
//            {
//                helper.AddUserToRole(model.User.Id, roleadd);
//            }
//            return RediretToAction("Index");
//        }





//    }
//}