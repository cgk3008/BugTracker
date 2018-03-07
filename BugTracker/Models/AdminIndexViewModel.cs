using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTracker.Models
{
    public class AdminIndexViewModel
    {

        public User User { get; set; }
        public ICollection<string> Roles { get; set; }



        public Project Project { get; set; }
        public ICollection<string> Users { get; set; }


    }
}