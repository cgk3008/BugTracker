using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

namespace BugTracker.Models.Helper
{
    public static class Extensions
    {
        private static ApplicationDbContext db = new ApplicationDbContext();

        //public static void AddPmAfterAssign(this Project pmAddToProject)
        //{
         
        //    List<Project> users = new List<Project>();
        //    User currentDbStateProjectPm = db.Project.Find(projectId).Users.ToList();

        //    if (pmAddToProject.PmId != currentDbStateProjectPm.)
        //    {
        //        users.Add(new Project()



        //    }

        //}


        //public Exception AddUserToProject(string userId, int projectId)
        //{
        //    try
        //    {
        //        var prj = dB.Project.Find(projectId);
        //        var usr = dB.Users.Find(userId);
        //        prj.Users.Add(usr); /*why didn't this work with prj.Users.Add(str);*/
        //        dB.SaveChanges();
        //        return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex;
        //    }
        //}
        //public static void PmIdToName(th
        //    is Project project)
        //{


        //}

        public static void CreateHistories(this Ticket editedTicket)
        {
            List<TicketHistory> histories = new List<TicketHistory>();
            Ticket currentDbStateTicket = db.Ticket.AsNoTracking().FirstOrDefault(t => t.Id == editedTicket.Id);

            if (editedTicket.Title != currentDbStateTicket.Title)
            {
                histories.Add(new TicketHistory()
                {
                    OldValue = currentDbStateTicket.Title,
                    NewValue = editedTicket.Title,
                    Property = "Title"
                });
                
            }

            if (editedTicket.Description != currentDbStateTicket.Description)
            {
                histories.Add(new TicketHistory()
                {
                    OldValue = currentDbStateTicket.Description,
                    NewValue = editedTicket.Description,
                    Property = "Description"
                });
            }

            if (editedTicket.AssignedToUserId != currentDbStateTicket.AssignedToUserId)
            {
                User previouslyAssignedUser = db.Users.Find(currentDbStateTicket.AssignedToUserId);
                User newlyAssignedUser = db.Users.Find(editedTicket.AssignedToUserId);

                histories.Add(new TicketHistory()
                {
                    // You'll want to use user properties that are required, so that you will know that there will be a value to be displayed
                    // Here I am using interpolated strings, using the FirstName and LastName properties to build the users' full name
                    // This assumes that these two properties are required
                    //OldValue = $"{previouslyAssignedUser.FirstName} {previouslyAssignedUser.LastName}",
                    //NewValue = $"{newlyAssignedUser.FirstName} {newlyAssignedUser.LastName}",
                    //Property = "Assigned User"

                    OldValue = previouslyAssignedUser.FullName,
                    NewValue = newlyAssignedUser.FullName,
                    Property = "Assigned User"
                });
            }

            if (editedTicket.TicketStatusId != currentDbStateTicket.TicketStatusId)
            {
                histories.Add(new TicketHistory()
                {
                    OldValue = db.Status.Find(currentDbStateTicket.TicketStatusId).Name,
                    NewValue = db.Status.Find(editedTicket.TicketStatusId).Name,
                    Property = "Ticket Status"
                });
            }

            if (editedTicket.TicketPriorityId != currentDbStateTicket.TicketPriorityId)
            {
                histories.Add(new TicketHistory()
                {
                    OldValue = db.Priority.Find(currentDbStateTicket.TicketPriorityId).Name,
                    NewValue = db.Priority.Find(editedTicket.TicketPriorityId).Name,
                    Property = "Ticket Priority"
                });
            }

            if (editedTicket.TicketTypeId != currentDbStateTicket.TicketTypeId)
            {
                histories.Add(new TicketHistory()
                {
                    OldValue = db.Type.Find(currentDbStateTicket.TicketTypeId).Name,
                    NewValue = db.Type.Find(editedTicket.TicketTypeId).Name,
                    Property = "Ticket Type"
                });
            }

            if (histories.Count > 0)
            {
                string userId = HttpContext.Current.User.Identity.GetUserId();

                for (int i = 0; i < histories.Count; i++)
                {
                    histories[i].UserId = userId;
                    histories[i].Changed = DateTimeOffset.Now;
                    histories[i].TicketId = editedTicket.Id;

                    db.History.Add(histories[i]);
                }

                db.SaveChanges();
            }
        }





        public static string GetFullName(this IIdentity user)
        {
            var ClaimsUser = (ClaimsIdentity)user;
            var claim = ClaimsUser.Claims.FirstOrDefault(c => c.Type == "Name");
            if (claim != null)
            {
                return claim.Value;

            }
            else
            {
                return null;
            }
        }

        public static bool In(this string source, params string[] list)
        {
            if (null == source) throw new ArgumentNullException("source");
            return list.Contains(source, StringComparer.OrdinalIgnoreCase);
        }
    }
}