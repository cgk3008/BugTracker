namespace BugTracker.Migrations
{
    using BugTracker.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;


    internal sealed class Configuration : DbMigrationsConfiguration<BugTracker.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            var store = new RoleStore<IdentityRole>(context);
            var manager = new RoleManager<IdentityRole>(store);
            var role = new IdentityRole();

            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                role = new IdentityRole { Name = "Admin" };
                manager.Create(role);
            }

            if (!context.Roles.Any(r => r.Name == "Developer"))
            {
                role = new IdentityRole { Name = "Developer" };
                manager.Create(role);
            }

            if (!context.Roles.Any(r => r.Name == "Submitter"))
            {
                role = new IdentityRole { Name = "Submitter" };
                manager.Create(role);
            }

            if (!context.Roles.Any(r => r.Name == "ProjectManager"))
            {
                role = new IdentityRole { Name = "ProjectManager" };
                manager.Create(role);
            }

            var userStore = new UserStore<User>(context);
            var userManager = new UserManager<User>(userStore);

            if (!context.Users.Any(u => u.Email == "cgk3008.ck@gmail.com"))
            {
                /*var user = new User*/
                userManager.Create(new User
                {
                    UserName = "cliffkoenig",
                    Email = "cgk3008.ck@gmail.com",
                    FirstName = "Cliff",
                    LastName = "Koenig",
                    DisplayName = "ADMIN"
                }, "Redd12!");
                //Add password above????

                //userManager.Create(user, "cliffkoenig");

                //userManager.AddToRoles(user.Id,
                //    new string[] {
                //        "Admin"
                //    });
            }

            var userId = userManager.FindByEmail("cgk3008.ck@gmail.com").Id;
            userManager.AddToRole(userId, "Admin");

            if (!context.Users.Any(u => u.Email == "moderator@coderfoundry.com"))
            {
                /* var user = new User*/
                userManager.Create(new User
                {
                    UserName = "moderator",
                    Email = "moderator@coderfoundry.com",
                    FirstName = "Antonio",
                    LastName = "Raynor",
                    DisplayName = "MANAGER"
                }, "Password-1");
                //Add password above????

                //userManager.Create(user, "Abc&123!");

                //userManager.AddToRoles(user.Id,
                //    new string[] {
                //        "ProjectManager"
                //    });
            }

            var userIdMod = userManager.FindByEmail("moderator@coderfoundry.com").Id;
            userManager.AddToRole(userIdMod, "ProjectManager");


            if (!context.Users.Any(u => u.Email == "developer@demo.com"))
            {
                var user = new User
                {
                    UserName = "developer@demo.com",
                    Email = "developer@demo.com",
                    FirstName = "Developer",
                    LastName = "Role",
                    DisplayName = "DEVPR"
                };

                //userManager.Create(user, "Abc&123!");

                //userManager.AddToRoles(user.Id,
                //    new string[] {
                //        "Developer"
                //    });
            }
            if (!context.Users.Any(u => u.Email == "submitter@demo.com"))
            {
                var user = new User
                {
                    UserName = "submitter@demo.com",
                    Email = "submitter@demo.com",
                    FirstName = "Submitter",
                    LastName = "Role",
                    DisplayName = "SUBMT"
                };

                userManager.Create(user, "Abc&123!");

                userManager.AddToRoles(user.Id,
                    new string[] {
                        "Submitter"
                    });
            }


            //if (!context.TicketPriorities.Any(u => u.Name == "High"))
            //{ context.TicketPriorities.Add(new TicketPriority { Name = "High" }); }

            //if (!context.TicketPriorities.Any(u => u.Name == "Medium"))
            //{ context.TicketPriorities.Add(new TicketPriority { Name = "Medium" }); }

            //if (!context.TicketPriorities.Any(u => u.Name == "Low"))
            //{ context.TicketPriorities.Add(new TicketPriority { Name = "Low" }); }

            //if (!context.TicketPriorities.Any(u => u.Name == "Urgent"))
            //{ context.TicketPriorities.Add(new TicketPriority { Name = "Urgent" }); }

            //if (!context.TicketTypes.Any(u => u.Name == "Production Fix"))
            //{ context.TicketTypes.Add(new TicketType { Name = "Production Fix" }); }

            //if (!context.TicketTypes.Any(u => u.Name == "Project Task"))
            //{ context.TicketTypes.Add(new TicketType { Name = "Project Task" }); }

            //if (!context.TicketTypes.Any(u => u.Name == "Software Update"))
            //{ context.TicketTypes.Add(new TicketType { Name = "Software Update" }); }

            //if (!context.TicketStatuses.Any(u => u.Name == "New"))
            //{ context.TicketStatuses.Add(new TicketStatus { Name = "New" }); }

            //if (!context.TicketStatuses.Any(u => u.Name == "In Development"))
            //{ context.TicketStatuses.Add(new TicketStatus { Name = "In Development" }); }

            //if (!context.TicketStatuses.Any(u => u.Name == "Completed"))
            //{ context.TicketStatuses.Add(new TicketStatus { Name = "Completed" }); }


        }
    }
}
