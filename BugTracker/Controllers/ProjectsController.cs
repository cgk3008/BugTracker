using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BugTracker.Models;
using BugTracker.Models.Helper;
using Microsoft.AspNet.Identity;

namespace BugTracker.Controllers
{
    public class ProjectsController : Controller
    {
        private ApplicationDbContext dB = new ApplicationDbContext();

        // GET: Projects
        public ActionResult Index()
        {
            return View(dB.Project.ToList());
        }

        // GET: Projects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = dB.Project.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // GET: Projects/Create
        public ActionResult Create()
        {
            return View();
        }



        // POST: Projects/Create


        //So let's make sure we can't create project with same name, AccountController under Register , Post, has code on registration that does this which gathers data from AcountViewModels


        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name, Created, pmId")] Project project)
        {
            //add project name check helper here...relate to fields above?

            if (ModelState.IsValid)
            {
                project.Created = DateTime.Now;
                dB.Project.Add(project);
                dB.SaveChanges();
                return RedirectToAction("Index");
            }

            //return View(project);
            return RedirectToAction("Index", "AdminProjects");
        }

        // GET: Projects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = dB.Project.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] Project project)
        {
            if (ModelState.IsValid)
            {
                dB.Entry(project).State = EntityState.Modified;
                dB.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(project);
        }

        // GET: Projects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = dB.Project.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Project project = dB.Project.Find(id);
            dB.Project.Remove(project);
            dB.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: MyProjects
        public ActionResult MyProjects()
        { var userId = User.Identity.GetUserId();
            //return View(dB.Users.Find(userId).Ticket.ToList());

            //example from assignedProjects controller return View(dB.Users.Find(userId).Project.ToList()); can't figure out why above does not work, need to look at ticket model more compared to project model

            return View(dB.Users.Find(userId).Project.ToList());
        }
        
        // Get tickets each project
        public ActionResult TicketsForEachProject()
        { 
        var userId = User.Identity.GetUserId();
        var UserProjects = dB.Project.Include("Ticket");
            //var projectHelper = new projectHelper();
            //var myProjects = projectHelper.ListUserProjects(userId);
            return View();
    }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                dB.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
