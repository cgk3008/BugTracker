using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTracker.Models.Helper
{
    public class ProjectHelper
    {

        //        Your project helper class should include but is not limited to...

        //IsUserOnProject
        //ListUserProjects-done
        //AddUserToProject-done
        //RemoveUserFromProject-done
        //UsersOnProject-donedB
        //UserNotOnProject(edited)-done?



            //dB.Project.Any (p => p.) "Any" returns a boolean??


        private ApplicationDbContext dB = new ApplicationDbContext();

        public Exception AddUserToProject(string userId, int projectId)
        {
            try
            {
                var prj = dB.Project.Find(projectId);
                var usr = dB.Users.Find(userId);
                prj.Users.Add(usr); /*why didn't this work with prj.Users.Add(str);*/
                dB.SaveChanges();
                return null;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public Exception RemoveUserFromProject(string userId, int projectId)
        {
            try
            {
                var prj = dB.Project.Find(projectId);
                var usr = dB.Users.Find(userId);
                prj.Users.Remove(usr);
                dB.SaveChanges();
                return null;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public ICollection<Project> ListProjectsForUser(string userId)
        {

            return dB.Users.Find(userId).Project.ToList();

            //all this code simplified above
            //var usr = dB.Users.Find(userId);
            //List<Project> ProjUsers = new List<Project>();

            //try
            //{
            //    var prj = dB.Project.ToList();
            //    foreach (var p in prj)
            //    {
            //        if(p.Users.Contains(usr))
            //        {
            //            ProjUsers.Add(p);
            //        }
            //    }
            //    return ProjUsers;             
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex);
            //    return null;
            //}                
        }
        //add function to not allow same user on same project twice!!! wait, code already is not allowing that due to multiselect list


        //IsUserOnProject

        public bool IsUserOnProject(string userId, int projectId)
        {
            try
            {
                var prj = dB.Project.Find(projectId);
                var usr = dB.Users.Find(userId);
                //prj.User.Find(usr);

                //var result = dB.Users.Find(userId);
                if (prj.Users != null)
                {

                }
                return true;
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        public ICollection<User> ListUsersOnProject(int projectId)
        {
            return dB.Project.Find(projectId).Users.ToList();
        }

        //UserNotOnProject(edited)

        public ICollection<string> ListUsersNotOnProject(int projectId)
        {
            List<string> usersNotProj = new List<string>();
            var users = dB.Users;

            foreach (var u in users)
            {
                if (!IsUserOnProject(u.Id, projectId))
                {
                    usersNotProj.Add(u.DisplayName);
                }
            }
            return usersNotProj;
        }

    }
}