using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTracker.Models
{
    public class DashboardViewModel
    {
        public IEnumerable<Project> Projects { get; set; }
        public IEnumerable<Ticket> Tickets { get; set; }
        public IEnumerable<TicketNotification> TicketNotifications { get; set; }

        //We'll start with that

        //public Project Project { get; set; }
        //public Project Name { get; set; }
        //public bool IsDeleted { get; set; }

        //public ICollection<string> Users { get; set; }
        //public ICollection<string> Roles { get; set; }

        //public Ticket Ticket { get; set; }
        //public TicketNotification Notification { get; set; }

        /*
         * This view model looks a little strange to me. Can you explain what you're wanting to do with each of these properties?
         * Ignore the ticketnotifications first, I also thought at one point I would need roles.  Isdeleted I think I can remove and use in the View. I thought I would need to pass the Name of the project, which is why I had a Name parameter.
         * Ok.
         * Your just going to be displaying projects and tickets on the dashboard right?
         * Sorry, I will add list of Notifications, but I have a lot to work on that before I proceeed.
         * That's fine. I will set up the notifications here so that you can use them when you're ready. Great!
         * So, just projects, tickets and notifications then?
         * yes
         * ok cool
         */


    }
}