using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTracker.Models.Helper
{
    public class UserRolesHelper
    {
        private UserManager<User> userManager = new UserManager<User>(new UserStore<User>(new ApplicationDbContext()));

        private ApplicationDbContext dB = new ApplicationDbContext();

        public bool IsUserInRole(string UserId, string Role)
        {
            try   /*use try catch statements whenever utlizing/clicking light bulb for  "using Microsoft.AspNet.Identity;" or other one you did not make. or write to external file that lists errors*/
            {
                var result = userManager.IsInRole(UserId, Role);
                return result;
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }

            //finally use if want to display or bundle/categorize errors

        }

        public bool AddUserToRole(string UserId, string Role)
        {
            try
            {
                var result = userManager.AddToRole(UserId, Role);
                return result.Succeeded;
            }
            catch
            {
                return false;
            }

        }

        public bool RemoveUserFromRole(string UserId, string Role)
        {
            try
            {
                var result = userManager.RemoveFromRole(UserId, Role);
                return result.Succeeded;
            }

            catch
            {
                return false;
            }
        }

        public ICollection<User> ListUsersInRole(string Role)
        {
            List<User> roleUsers = new List<User>();
            List<User> users = userManager.Users.ToList();

            foreach (var u in users)
            {
                if (IsUserInRole(u.Id, Role))
                {
                    roleUsers.Add(u);
                }
            }
            return roleUsers;
        }

        public ICollection<User> ListUsersNotInRole(string Role)
        {
            List<User> roleUsers = new List<User>();
            List<User> users = userManager.Users.ToList();

            foreach (var u in users)
            {
                if (!IsUserInRole(u.Id, Role))
                {
                    roleUsers.Add(u);
                }
            }
            return roleUsers;
        }


        //public ICollection<User> ListRolesForUser(string UserId)

        //{
        //  try
        //    {
        //        return userManager.GetRoles(UserId);

        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}

        //Need list of roles not assigned to user


    }
}