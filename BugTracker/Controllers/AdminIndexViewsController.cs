﻿using BugTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BugTracker.Controllers
{
    public class AdminIndexViewsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {




            //var blogComments = dB.Comments.Include(b => b.Author).Include(b => b.Post);
            return View();
        }

}
}