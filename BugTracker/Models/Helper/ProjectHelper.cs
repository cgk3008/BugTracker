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
        //UsersOnProject-done
        //UserNotOnProject(edited)-done?



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

            //all this code simplified above
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
        //add function to not allow same user on same project twice!!!


        //IsUserOnProject

        //public bool IsUserOnProject(string userId, int projectId)
        //{
        //    try
        //    {
        //        var prj = db.Project.Find(projectId);
        //        var usr = db.Users.Find(userId);
        //        prj.User.Find(usr);

        //        var result = db.Users.Find(userId);
        //        if (
        //        result != null)
        //            return true;
        //    }

        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex);
        //        return false;
        //    }
        //}

        public ICollection<User> ListUsersOnProject(int projectId)
        {
            return db.Project.Find(projectId).User.ToList();
        }

        //UserNotOnProject(edited)

        //    public ICollection<User> ListUsersNotOnProject(int projectId)
        //    {
        //        List<User> projectUsers = new List<User>();
        //        List<User> users = db.Users.ToList();

        //        foreach (var u in users)
        //        {
        //            if (!IsUserOnProject(u.Id, projectId))
        //            {
        //                projectUsers.Add(u);
        //            }
        //        }
        //        return projectUsers;
        //    }


    }
}