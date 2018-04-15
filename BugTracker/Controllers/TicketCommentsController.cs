using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BugTracker.Models;
using Microsoft.AspNet.Identity;

namespace BugTracker.Controllers
{
    public class TicketCommentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TicketComments
        public ActionResult Index()
        {

            if (User.IsInRole("Admin"))

            {
                var ticketComment = db.Comment.Include(t => t.Ticket).Include(t => t.User);
                return View(ticketComment.ToList());

            }

            if (User.IsInRole("Project Manager") || User.IsInRole("Submitter") || User.IsInRole("Developer"))

            {
                return RedirectToAction("MyTickets", "Tickets");
            }

            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: TicketComments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketComment ticketComment = db.Comment.Find(id);
            if (ticketComment == null)
            {
                return HttpNotFound();
            }
            return View(ticketComment);
        }

        // GET: TicketComments/Create
        public ActionResult Create()
        {
            ViewBag.TicketId = new SelectList(db.Ticket, "Id", "Body");
            ViewBag.UserId = new SelectList(db.Users, "Id", "FullName");

            return View();
        }

        // POST: TicketComments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Body,Created,TicketId,UserId,FileUrl")] TicketComment ticketComment, Ticket ticket, HttpPostedFileBase image)
      
        {
            if (ModelState.IsValid)
            {
                if (ImageUploadValidator.IsWebFriendlyImage(image))
                {
                    var fileName = Path.GetFileName(image.FileName);
                    image.SaveAs(Path.Combine(Server.MapPath("~/UploadsComments/"), fileName));
                    ticketComment.FileUrl = "/UploadsComments/" + fileName;
                }

                ticketComment.UserId = User.Identity.GetUserId();
                ticketComment.Created = DateTime.Now;
                db.Comment.Add(ticketComment);
                db.SaveChanges();
                var tix = db.Comment.Include("Ticket").FirstOrDefault(c => c.Id == ticketComment.Id);


                var callbackUrl = Url.Action("Details", "Tickets", new { id = ticketComment.Id }, protocol: Request.Url.Scheme);

                try
                {
                    EmailService ems = new EmailService();
                    IdentityMessage msg = new IdentityMessage();
                    //User user = db.Users.Find(model.AssignedToUserId);
                    User user = db.Users.Find(ticket.AssignedToUserId);

                    msg.Body = "New Ticket Comment." + Environment.NewLine + "Please click the following link to view the details " + "<a href=\"" + callbackUrl + "\">CHANGE TO YOUR COMMENTS ON YOUR TICKET</a>";

                    msg.Destination = user.Email;
                    msg.Subject = "Changes to your ticket";
                    await ems.SendMailAsync(msg);
                }
                catch (Exception ex)
                {
                    await Task.FromResult(0);
                }
                
                return RedirectToAction("Details", "Tickets", new { id = ticketComment.TicketId });
                //return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "FullName", ticketComment.UserId);
            ViewBag.TicketId = new SelectList(db.Ticket, "Id", "Body", ticketComment.TicketId);
            //return RedirectToAction("Details", "Tickets", new { ticket = tix.TicketId });

            //This redirect to detail page with comments is not working
            return RedirectToAction("Details", "Tickets", new { id = ticketComment.TicketId });
            //return View(ticketComment);
        }

        // GET: TicketComments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketComment ticketComment = db.Comment.Find(id);
            if (ticketComment == null)
            {
                return HttpNotFound();
            }
            ViewBag.TicketId = new SelectList(db.Ticket, "Id", "Body", ticketComment.TicketId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "FullName", ticketComment.UserId);
            return View(ticketComment);
        }

        // POST: TicketComments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Body,Created,TicketId,UserId,FileUrl")] TicketComment ticketComment, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (ImageUploadValidator.IsWebFriendlyImage(image))
                {
                    var fileName = Path.GetFileName(image.FileName);
                    image.SaveAs(Path.Combine(Server.MapPath("~/UploadsComments/"), fileName));
                    ticketComment.FileUrl = "/UploadsComments/" + fileName;
                }


                //the created DateTime.Now gets set to zero. Update time works, but my edit method is changing Created. Do I take it out of Bind statement above

                ticketComment.UserId = User.Identity.GetUserId();
                ticketComment.Updated = DateTime.Now;
                db.Entry(ticketComment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "Tickets", new { id = ticketComment.TicketId });
            }
            ViewBag.TicketId = new SelectList(db.Ticket, "Id", "Body", ticketComment.TicketId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "FullName", ticketComment.UserId);
            return RedirectToAction("Details", "Tickets", new { id = ticketComment.TicketId });

        }

        // GET: TicketComments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketComment ticketComment = db.Comment.Find(id);
            if (ticketComment == null)
            {
                return HttpNotFound();
            }
            return View(ticketComment);
        }

        // POST: TicketComments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TicketComment ticketComment = db.Comment.Find(id);
            db.Comment.Remove(ticketComment);
            db.SaveChanges();
            return RedirectToAction("Details", "Tickets", new { id = ticketComment.TicketId });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
