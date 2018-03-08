using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BugTracker.Models
{
    public class AdminProject
    {

        public Project Project { get; set; }
       
        public MultiSelectList Users { get; set; }  //change Users to User?????
        public string[] SelectedUsers { get; set; }
        public User RmvUser { get; set; }



    }
}

//public class AdminIndexViewModel in model folder
//{

//    public User User { get; set; }
//    public ICollection<string> Roles { get; set; }



//    public Project Project { get; set; }
//    public ICollection<string> Users { get; set; }


//}