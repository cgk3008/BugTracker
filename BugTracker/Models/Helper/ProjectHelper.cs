using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTracker.Models.Helper
{
    public class ProjectHelper
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        public Exception AddUserToProject(string userId, int projectId)
        {
            try
            {
                var prj = db.Project.Find(projectId);
                var usr = db.Users.Find(userId);
                prj.User.Add(usr); /*why didn't this work with prj.Users.Add(str);*/
                db.SaveChanges();
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
                var prj = db.Project.Find(projectId);
                var usr = db.Users.Find(userId);
                prj.User.Remove(usr);
                db.SaveChanges();
                return null;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }


        public ICollection<Project> ListProjectsForUser(string userId)
        {

            return db.Users.Find(userId).Project.ToList();

            //var usr = db.Users.Find(userId);
            //List<Project> ProjUsers = new List<Project>();

            //try
            //{
            //    var prj = db.Project.ToList();
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



    }
}