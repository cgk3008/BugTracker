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
        public string FullName { get; set; }
        //public string Password { get; set; }

        public virtual ICollection<Project> Project { get; set; }
        public virtual ICollection<Ticket> Ticket { get; set; }
        public virtual ICollection<TicketComment> Comment { get; set; }
        public virtual ICollection<TicketAttachment> Attachment { get; set; }
        public virtual ICollection<TicketHistory> History { get; set; }
        public virtual ICollection<TicketNotification> Notification { get; set; }
        //public virtual ICollection<Ticket> Ticket { get; set; } would create extra user underscore IDs
        //public virtual ICollection<Ticket> OwnerUser { get; set; } Don't need this one!!!!
        //public virtual ICollection<Ticket> AssignedToUser { get; set; }   Don't need this one!!!!    

        public User()
        {
            Project = new HashSet<Project>();       
            Ticket = new HashSet<Ticket>();
            Comment = new HashSet<TicketComment>();
            Attachment = new HashSet<TicketAttachment>();
            History = new HashSet<TicketHistory>();
            Notification = new HashSet<TicketNotification>();
            //OwnerUser = new HashSet<Ticket>();
            //AssignedToUser = new HashSet<Ticket>();
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

        public DbSet<Ticket> Ticket { get; set; }
        public DbSet<TicketComment> Comment { get; set; }
        public DbSet<TicketAttachment> Attachment { get; set; }
        public DbSet<TicketHistory> History { get; set; }
        public DbSet<TicketNotification> Notification { get; set; }
        public DbSet<TicketType> Type { get; set; }
        public DbSet<TicketStatus> Status { get; set; }
        public DbSet<TicketPriority> Priority { get; set; }
        public DbSet<Project> Project { get; set; }


    }
}