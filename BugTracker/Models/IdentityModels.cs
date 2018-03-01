using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BugTracker.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class User : IdentityUser
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }

        //public virtual Project Project { get; set; }
        public virtual ICollection<Project> Project { get; set; }
        public virtual ICollection<TicketComment> Comment { get; set; }
        public virtual ICollection<TicketAttachment> Attachment { get; set; }
        public virtual ICollection<TicketHistory> History { get; set; }
        public virtual ICollection<TicketNotification> Notification { get; set; }
        //public virtual ICollection<Ticket> Ticket { get; set; }
        public virtual ICollection<Ticket> OwnerUser { get; set; }
        public virtual ICollection<Ticket> AssignedToUser { get; set; }

       

        public User()
        {
            Project = new HashSet<Project>();
            Comment = new HashSet<TicketComment>();
            Attachment = new HashSet<TicketAttachment>();
            History = new HashSet<TicketHistory>();
            Notification = new HashSet<TicketNotification>();
            OwnerUser = new HashSet<Ticket>();
            AssignedToUser = new HashSet<Ticket>();
            //Ticket = new HashSet<Ticket>();
        }


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {

        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}