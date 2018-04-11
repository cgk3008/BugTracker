using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BugTracker.Models
{
    public class Project
    {
        public int Id { get; set; }
        [StringLength(100, ErrorMessage = "The {0} cannot be {1} characters long.")]
        public string Name { get; set; }
        public DateTimeOffset Created { get; set; }
        public string PmId { get; set; }
        public bool IsDeleted { get; set; } //wait was this supposed to be a boolean????
                                            //public virtual User User { get; set; } -don't need this, the ICollection does this when we have one ICollections below referenced to User file and ICollection referenceing project in the User file.
        [Range(0, 100)]
        public int Progress { get; set; } //restrict to 0-100
        public virtual ICollection<Ticket> Ticket { get; set; }
        public virtual ICollection<User> Users { get; set; }

        public Project()
        {
            Users = new HashSet<User>();
            Ticket = new HashSet<Ticket>();
        }
    }
}