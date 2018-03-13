using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BugTracker.Models
{
    public class AdminModel
    {
        //[Key]
        //public int Id { get; set; }
      
        public User User { get; set; }
        //public List<string> Roles { get; set; }
        public MultiSelectList Roles { get; set; }
        public string[] SelectedRoles { get; set; }

    }


    //public ActionResult
}


//public class AdminIndexViewModel in model folder
//{

//    public User User { get; set; }
//    public ICollection<string> Roles { get; set; }



//    public Project Project { get; set; }
//    public ICollection<string> Users { get; set; }


//}