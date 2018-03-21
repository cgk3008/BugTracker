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
    public class TicketsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Tickets

        public ActionResult Index()
        {
            var tickets = db.Ticket.Include(t => t.AssignedToUser).Include(t => t.OwnerUser).Include(t => t.Priority).Include(t => t.Project).Include(t => t.Status).Include(t => t.Type);
            return View(tickets.ToList());
        }


        // GET: Tickets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Ticket.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // GET: Tickets/Create

        public ActionResult Create()
        {
            var userId = User.Identity.GetUserId();
            ProjectHelper helper = new ProjectHelper();
            var projlist = helper.ListProjectsForUser(userId);
            //Ticket.AssignedToUserId = new MultiSelectList(db, "id", "FullName", devlist);

            ViewBag.ProjectId = new SelectList(projlist, "Id", "Name");

            ViewBag.AssignedToUserId = new SelectList(db.Users, "Id", "FullName");
            ViewBag.OwnerUserId = new SelectList(db.Users, "Id", "FullName");
            ViewBag.TicketPriorityId = new SelectList(db.Priority, "Id", "Name");
            //ViewBag.ProjectId = new SelectList(db.Project, "Id", "Name");
            ViewBag.TicketStatusId = new SelectList(db.Status, "Id", "Name");
            ViewBag.TicketTypeId = new SelectList(db.Type, "Id", "Name");
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Description,Created,Updated,ProjectId,TicketTypeId,TicketPriorityId,TicketStatusId,OwnerUserId,AssignedToUserId")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                ticket.OwnerUserId = User.Identity.GetUserId();
                ticket.Created = DateTime.Now;
                db.Ticket.Add(ticket);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AssignedToUserId = new SelectList(db.Users, "Id", "FullName", ticket.AssignedToUserId);
            //ViewBag.OwnerUserId = new SelectList(db.Users, "Id", "FullName", ticket.OwnerUserId);
            ViewBag.TicketPriorityId = new SelectList(db.Priority, "Id", "Name", ticket.TicketPriorityId);
            ViewBag.ProjectId = new SelectList(db.Project, "Id", "Name", ticket.ProjectId);
            ViewBag.TicketStatusId = new SelectList(db.Status, "Id", "Name", ticket.TicketStatusId);
            ViewBag.TicketTypeId = new SelectList(db.Type, "Id", "Name", ticket.TicketTypeId);
            return View(ticket);
        }

        // GET: Tickets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Ticket.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }

            //AdminModel AdminModel = new AdminModel();
            UserRolesHelper helper = new UserRolesHelper();
            var devlist = helper.ListUsersInRole("Developer");
            //Ticket.AssignedToUserId = new MultiSelectList(db, "id", "FullName", devlist);



            ViewBag.AssignedToUserId = new SelectList(devlist, "Id", "FullName", ticket.AssignedToUserId);
            ViewBag.OwnerUserId = new SelectList(db.Users, "Id", "FullName", ticket.OwnerUserId);
            ViewBag.TicketPriorityId = new SelectList(db.Priority, "Id", "Name", ticket.TicketPriorityId);
            ViewBag.ProjectId = new SelectList(db.Project, "Id", "Name", ticket.ProjectId);
            ViewBag.TicketStatusId = new SelectList(db.Status, "Id", "Name", ticket.TicketStatusId);
            ViewBag.TicketTypeId = new SelectList(db.Type, "Id", "Name", ticket.TicketTypeId);
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Description,Created,Updated,ProjectId,TicketTypeId,TicketPriorityId,TicketStatusId,OwnerUserId/*,AssignedToUserId*/")] Ticket model)
        {
            if (ModelState.IsValid)
            {
                var oldTicket = db.Ticket.AsNoTracking().FirstOrDefault(t => t.Id == model.Id);

                foreach (var prop in typeof(Ticket).GetProperties())
                {
                    if (prop.Name != null && prop.Name.In("Title", "Description", "TicketTypeId", "TicketPriorityId", "TicketStatusId"/*, "AssignedToUserId"*/))
                    {
                        var oldVal = oldTicket.GetType().GetProperty(prop.Name).GetValue(oldTicket).ToString();
                        var newVal = model.GetType().GetProperty(prop.Name).GetValue(model).ToString();

                        if (oldVal != newVal)
                        {
                            TicketHistory ticketHistory = new TicketHistory()
                            {
                                TicketId = oldTicket.Id,
                                UserId = User.Identity.GetUserId(),
                                Property = prop.Name,
                                OldValue = oldVal,
                                NewValue = newVal,

                                Changed = DateTime.Now
                            };
                            db.History.Add(ticketHistory);
                        }
                    }
                }

                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            ViewBag.OwnerUserId = new SelectList(db.Users, "Id", "FullName", model.OwnerUserId);
            ViewBag.TicketPriorityId = new SelectList(db.Priority, "Id", "Name", model.TicketPriorityId);
            ViewBag.ProjectId = new SelectList(db.Project, "Id", "Name", model.ProjectId);
            ViewBag.TicketStatusId = new SelectList(db.Status, "Id", "Name", model.TicketStatusId);
            ViewBag.TicketTypeId = new SelectList(db.Type, "Id", "Name", model.TicketTypeId);
            return View(model);

        }

        //if (ModelState.IsValid)
        //{
        //    ticket.OwnerUserId = User.Identity.GetUserId();
        //    ticket.Updated = DateTime.Now;
        //    db.Entry(ticket).State = EntityState.Modified;
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}


        //if (ModelState.IsValid)
        //{
        //   
        //    var oldTic = db.Ticket.AsNoTracking().FirstOrDefault(t => t.Id == ticket.Id);
        //    foreach (var prop in typeof(Ticket).GetProperties())
        //    {
        //        
        //        if (prop.Name != null)

        //            //if (prop.Name != null && prop.Name.IndexOf("Description", "TicketComments", "Title", "Notifications"))
        //            //need to change Body to Title
        //            var OldValue = oldTic.GetType().GetField(prop.Name).ToString();
        //        var NewValue = ticket.GetType().GetField(prop.Name).ToString();
        //        
        //if (OldValue != NewValue_)
        //        {
        //            
        //            TicketHistory ticketHistory = new TicketHistory()
        //            {
        //                TicketId = ticket.Id,
        //                UserId = User.Identity.GetUserId(),
        //                Property = prop.Name,
        //                OldValue = oldTic.GetType().GetField(prop.Name).ToString(),
        //                NewValue = ticket.GetType().GetField(prop.Name).ToString(),
        //                Changed = DateTime.Now
        //            };
        //            db.TicketHistories.Add(ticketHistory);
        //            db.SaveChanges();
        //        }

        //    }
        //    return RedirectToAction("Details", new { id = ticket.Id });
        //}


        //ViewBag.AssignedToUserId = new SelectList(db.Users, "Id", "FullName", ticket.AssignedToUserId);




        // GET: Tickets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Ticket.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ticket ticket = db.Ticket.Find(id);
            db.Ticket.Remove(ticket);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: My Tickets
        public ActionResult MyTickets()
        {
            var userId = User.Identity.GetUserId();

            var proj = db.Users.Find(userId).Project;

            if (User.IsInRole("Project Manager"))
            {
                //var tickets = db.Ticket.Where(p => p.ProjectId == proj).Include(t => t.AssignedToUser).Include(t => t.OwnerUser).Include(t => t.Project).Include(t => t.Priority).Include(t => t.Status).Include(t => t.Type);
                
                var tkts = db.Project.Where(p => p.PmId == userId).SelectMany(t => t.Ticket).ToList();

                //var tkts = db.Project.Where(p => p.PmId == userId).SelectMany(t => t.Ticket).Include(tx => tx.AssignedToUser).Include(tx => tx.OwnerUser).Include(tx => tx.Project).Include(tx => tx.Priority).Include(tx => tx.Status).Include(tx => tx.Type);

              
                return View(tkts.ToList());
            }

                //    TicketHelper helper = new TicketHelper();
                //    var projtickets = helper.GetProjectTickets().;


                //    GetProjectTickets(int projectId)

                //    //return db.Project.Find(projectId).Ticket.ToList();

                //}               

                if (User.IsInRole("Developer"))
                {
                    var tickets = db.Ticket.Where(u => u.AssignedToUserId == userId).Include(t => t.AssignedToUser).Include(t => t.OwnerUser).Include(t => t.Project).Include(t => t.Priority).Include(t => t.Status).Include(t => t.Type);
                    return View(tickets.ToList());
                }

                if (User.IsInRole("Submitter"))
                {
                    var tickets = db.Ticket.Where(u => u.OwnerUserId == userId).Include(t => t.AssignedToUser).Include(t => t.OwnerUser).Include(t => t.Project).Include(t => t.Priority).Include(t => t.Status).Include(t => t.Type);
                    return View(tickets.ToList());
                }
                else //temperory else statement until i get tickethelper to work
                {
                    return View();
                }
                //return View(tickets.ToList());
            }

        //Get Assign Ticket
        public ActionResult AssignTicket(int? id)
        {
            //TicketRoleUserViewModel vm = new TicketRoleUserViewModel();
            UserRolesHelper helper = new UserRolesHelper();

            var ticket = db.Ticket.Find(id);

            var users = helper.ListUsersInRole("Developer").ToList();

            ViewBag.AssignedToUserId = new SelectList(users, "Id", "FullName", ticket.AssignedToUserId);

            return View(ticket);
        }

        //Post Assign a ticket thru email notification
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AssignTicket (Ticket model)
        {
            var ticket = db.Ticket.Find(model.Id);
            ticket.AssignedToUserId = model.AssignedToUserId;
            db.SaveChanges();
            var callbackUrl = Url.Action("Details", "Tickets", new { id = ticket.Id }, protocol: Request.Url.Scheme);

            try
            {
                EmailService ems = new EmailService();
                IdentityMessage msg = new IdentityMessage();
                //User user = db.Users.Find(model.AssignedToUserId);
                User user = db.Users.Find(model.AssignedToUserId);

                msg.Body = "You have been assigned a new Ticket." + Environment.NewLine + "Please click the following link to view the details" + "<a href=\"" + callbackUrl + "\">NEW TICKET</a>";

                msg.Destination = user.Email;
                msg.Subject = "Assigned Ticket";
                await ems.SendMailAsync(msg);
            }
            catch (Exception ex)
            {
                await Task.FromResult(0);
            }
            return RedirectToAction("MyTickets");
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
