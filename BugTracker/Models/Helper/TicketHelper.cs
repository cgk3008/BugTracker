using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTracker.Models.Helper
{
    public class TicketHelper
    {
        public static ApplicationDbContext db = new ApplicationDbContext();
        public List<Ticket> GetProjectTickets(string userId, int projectId)
        {

            //var usr = db.Users.Find(userId);
            //var prj = db.Project.Find(projectId);

            //return db.prj.Users.Ticket.ToList();

            return db.Project.Find(userId, projectId).Ticket.ToList();



            //public ICollection<Project> ListProjectsForUser(string userId)
            //{

            //    return dB.Users.Find(userId).Project.ToList();

            //return db.Ticket.Where(t => t.ProjectId == projectId);



            //public Exception AddUserToProject(string userId, int projectId)
            //{
            //    try
            //    {
            //        var prj = dB.Project.Find(projectId);
            //       
            //        prj.Users.Add(usr); /*why didn't this work with prj.Users.Add(str);*/
            //        dB.SaveChanges();
            //        return null;
            //    }
            //    catch (Exception ex)
            //    {
            //        return ex;
            //    }
            //}





        }




        //public ICollection<User> ListUsersOnProject(int projectId)
        //{
        //    return dB.Project.Find(projectId).Users.ToList();
        //}



        //var userId = User.Identity.GetUserId();
        //var tickets = dB.Ticket.Where(u => u.AssignedToUserId == userId).Include(t => t.AssignedToUser).Include(t => t.OwnerUser).Include(t => t.Project).Include(t => t.Priority).Include(t => t.Status).Include(t => t.Type);

    }
}