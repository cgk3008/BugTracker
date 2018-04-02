using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTracker.Models.Helper
{
    public class ProjectHelper
    {


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

        public ICollection<User> ListUsersOnProject(int projectId)
        {
            return dB.Project.Find(projectId).Users.ToList();
        }

        public ICollection<User> ListPmForProject(int projectId)
            //do i need to User above to "string"?
        {
            var pm = dB.Project.Find(projectId).PmId;
            var usr = dB.Users.Find(pm).FullName;
            //dB.Users.Find(pm).FullName;

            return null;
        }

        public ICollection<Project> ListProjectsForUser(string userId)
        {

            //var isnotdeleted = dB.Project.Where(p => p.IsDeleted == false).ToList();
            //var proj = isnotdeleted.Users.Find(userId).
            //    var projects = dB.Users.Find(userId).Project.ToList();

            //if (projects != null)
            //{
            //    projects.
            //}

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