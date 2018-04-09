using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
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
            List<Project> projects = dB.Project.ToList();
            List<ProjectIndexViewModel> vms = new List<ProjectIndexViewModel>();
            var userId = User.Identity.GetUserId();
            foreach (Project project in projects)
            {
                ProjectIndexViewModel vm = new ProjectIndexViewModel()
                {
                    Project = project,
                    ProjectManager = dB.Users.Find(project.PmId),

                    UserId = userId
                };

                vms.Add(vm);
            }
            return View(vms);
        }

        // GET: Soft Delete Projects Index
        public ActionResult SoftDeleteIndex()
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
            var proj = id;
            //var tix = dB.Ticket.Where()
            //var tix = dB.Project.Where(t => t.Id == proj).SelectMany(t => t.Ticket).ToList();

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
        public ActionResult Create([Bind(Include = "Id, Name, Created, PmId, IsDeleted")] Project project)
        {
            //add project name check helper here...relate to fields above?

            if (ModelState.IsValid)
            {
                project.Created = DateTime.Now;
                project.IsDeleted = false;
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
            UserRolesHelper helper = new UserRolesHelper();
            var pmlist = helper.ListUsersInRole("ProjectManager, Admin");

            ViewBag.pmId = new SelectList(pmlist, "Id", "FullName");

            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id, Name, PmId")] Project project)
        {
            if (ModelState.IsValid)
            {
                dB.Entry(project).State = EntityState.Modified;


                dB.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.pmId = new SelectList(dB.Project, "Id", "FullName", project.PmId);
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

        // GET: Projects/SoftDelete
        public ActionResult SoftDelete(int? id)
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

        // POST: Projects/SoftDelete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SoftDelete([Bind(Include = "Id, Name, PmId, IsDeleted")] Project project)
        {
            if (ModelState.IsValid)
            {

                dB.Entry(project).State = EntityState.Modified;
                project.IsDeleted = true;
                dB.SaveChanges();
                return RedirectToAction("Index");

            }

            return View(project);
        }


        // GET: MyProjects
        [Authorize]
        public ActionResult MyProjects()
        {

            List<ProjectIndexViewModel> vms = new List<ProjectIndexViewModel>();
            var userId = User.Identity.GetUserId();
            List<Project> projects = dB.Users.Find(userId).Project.ToList();

            foreach (Project project in projects)
            {
                ProjectIndexViewModel vm = new ProjectIndexViewModel()
                {
                    Project = project,
                    ProjectManager = dB.Users.Find(project.PmId),

                    UserId = userId
                };

                vms.Add(vm);
            }
            return View(vms);

            //var tickets = db.Ticket.Where(t => t.Project.PmId == userId).ToList();
            //return View(tickets.ToList());



            //var userId = User.Identity.GetUserId();
            ////return View(dB.Users.Find(userId).Ticket.ToList());

            ////example from assignedProjects controller return View(dB.Users.Find(userId).Project.ToList()); can't figure out why above does not work, need to look at ticket model more compared to project model

            //return View(dB.Users.Find(userId).Project.ToList());
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

        //GET: Assign Project
        public ActionResult AssignProject(int? id)
        {

            UserRolesHelper helper = new UserRolesHelper();

            var project = dB.Project.Find(id);

            var users = helper.ListUsersInRole("Project Manager").ToList();

            ViewBag.PmId = new SelectList(users, "Id", "FullName", project.PmId);

            return View(project);
        }

        //POST: Assign a project thru email notification
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AssignProject(Project model)
        {
            var project = dB.Project.Find(model.Id);
            project.PmId = model.PmId;

            //var users = 

            //public Exception AddUserToProject(string userId, int projectId)
            //{
            //    try
            //    {
            //        var prj = dB.Project.Find(projectId);
            //        var usr = dB.Users.Find(userId);
            //        prj.Users.Add(usr); /*why didn't this work with prj.Users.Add(str);*/
            //        dB.SaveChanges();
            //        return null;
            //    }
            //    catch (Exception ex)
            //    {
            //        return ex;
            //    }
            //}




            dB.SaveChanges();
            var callbackUrl = Url.Action("Details", "Projects", new { id = project.Id }, protocol: Request.Url.Scheme);

            try
            {
                EmailService ems = new EmailService();
                IdentityMessage msg = new IdentityMessage();

                User user = dB.Users.Find(model.PmId);

                msg.Body = "New Project Assignment." + Environment.NewLine + "Please click the following link to view the details " + "<a href=\"" + callbackUrl + "\">NEW PROJECT</a>";

                msg.Destination = user.Email;
                msg.Subject = "Assigned Project";
                await ems.SendMailAsync(msg);
            }
            catch (Exception ex)
            {
                await Task.FromResult(0);
            }
            return RedirectToAction("Index");
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
