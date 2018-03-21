using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTracker.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTimeOffset Created { get; set; }
        public string PmId { get; set; }
        public string SoftDelete { get; set; } //wait was this supposed to be a boolean????
        //public virtual User User { get; set; } -don't need this, the ICollection does this when we have one ICollections below referenced to User file and ICollection referenceing project in the User file.

        public virtual ICollection<Ticket> Ticket { get; set; }
        public virtual ICollection<User> Users { get; set; }

        public Project()
        {
            Users = new HashSet<User>();
            Ticket = new HashSet<Ticket>();
        }
    }
}