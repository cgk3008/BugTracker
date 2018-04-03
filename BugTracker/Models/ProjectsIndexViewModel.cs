using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTracker.Models
{
    public class ProjectIndexViewModel
    {
        public Project Project { get; set; }
        public User ProjectManager { get; set; }
        //public User UserId { get; set; }
        public string UserId { get; set; }
    }
}