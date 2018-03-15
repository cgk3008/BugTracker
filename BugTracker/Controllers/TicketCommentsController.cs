﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
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
            var ticketComment = db.Comment.Include(t => t.Ticket).Include(t => t.User);
            return View(ticketComment.ToList());
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
        public ActionResult Create([Bind(Include = "Id,Body,Created,TicketId,UserId,FileUrl")] TicketComment ticketComment, HttpPostedFileBase image)
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

                //if (User.IsInRole("Admin, Moderator"))
                //{

                //}
                //else
                //{

                //}
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
                return RedirectToAction("Index");
            }
            ViewBag.TicketId = new SelectList(db.Ticket, "Id", "Body", ticketComment.TicketId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "FullName", ticketComment.UserId);
            return View(ticketComment);

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
            return RedirectToAction("Index");
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
