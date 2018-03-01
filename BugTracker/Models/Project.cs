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

        //public virtual User User { get; set; } -don't need this, the ICollection does this when we have one ICollections below referenced to User file and ICollection referenceing project in the User file.

        //public virtual ICollection<Ticket> Ticket { get; set; }
        public virtual ICollection<User> User { get; set; }

        public Project()
        {
            User = new HashSet<User>();
            //Ticket = new HashSet<Ticket>();
        }

      


    }
}