using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTracker.Models
{
    public class Ticket
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset? Updated { get; set; }
        public int ProjectId { get; set; }
        public int TicketTypeId { get; set; }
        public int TicketPriorityId { get; set; }
        public int TicketStatusId { get; set; }
        public string OwnerUserId { get; set; }
        public string AssignedToUserId { get; set; }
        public bool IsDeleted { get; set; }

        public virtual TicketType Type { get; set; }
        public virtual TicketPriority Priority { get; set; }
        public virtual TicketStatus Status { get; set; }
        public virtual Project Project { get; set; }
        public virtual User OwnerUser { get; set; }
        public virtual User  AssignedToUser { get; set; }

        public virtual ICollection<TicketComment> Comment { get; set; }
        public virtual ICollection<TicketAttachment> Attachment { get; set; }
        public virtual ICollection<TicketHistory> History { get; set; }
        public virtual ICollection<TicketNotification> Notification { get; set; }
        //public virtual ICollection<Project> Project { get; set; }

        public Ticket()
        {
            Comment = new HashSet<TicketComment>();
            Attachment = new HashSet<TicketAttachment>();
            History = new HashSet<TicketHistory>();
            Notification = new HashSet<TicketNotification>();
            //Project = new HashSet<Project>();
        }
        
    }
}