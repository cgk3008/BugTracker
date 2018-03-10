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

            if (!context.Roles.Any(r => r.Name == "Project Manager"))
            {
                role = new IdentityRole { Name = "Project Manager" };
                manager.Create(role);
            }

            var userStore = new UserStore<User>(context);
            var userManager = new UserManager<User>(userStore);

            if (!context.Users.Any(u => u.Email == "cgk3008.ck@gmail.com"))
            {
                var user = new User
                
                {
                    UserName = "cliffkoenig",
                    Email = "cgk3008.ck@gmail.com",
                    FirstName = "Cliff",
                    LastName = "Koenig",
                    DisplayName = "ADMIN",
                    FullName = "Cliff Koenig"
                };
             

                userManager.Create(user, "Redd12!");

                userManager.AddToRoles(user.Id,
                    new string[] {
                        "Admin"
                    });
            }

           

            if (!context.Users.Any(u => u.Email == "moderator@coderfoundry.com"))
            {
                var user = new User
                
                {
                    UserName = "moderator",
                    Email = "moderator@coderfoundry.com",
                    FirstName = "Antonio",
                    LastName = "Raynor",
                    DisplayName = "Antonio Raynor",
                    FullName = "Antonio Raynor"
                   
                };
               

                userManager.Create(user, "Abc&123!");

                userManager.AddToRoles(user.Id,
                    new string[] {
                        "ProjectManager"
                    });
            }

            if (!context.Users.Any(u => u.Email == "developer@demo.com"))
            {
                var user = new User
                {
                    UserName = "developer@demo.com",
                    Email = "developer@demo.com",
                    FirstName = "Developer",
                    LastName = "Role",
                    DisplayName = "DEVPR",
                    FullName = "Dev Role"
                };

                userManager.Create(user, "Abc&123!");

                userManager.AddToRoles(user.Id,
                    new string[] {
                        "Developer"
                    });
            }
            if (!context.Users.Any(u => u.Email == "submitter@demo.com"))
            {
                var user = new User
                {
                    UserName = "submitter@demo.com",
                    Email = "submitter@demo.com",
                    FirstName = "Submitter",
                    LastName = "Role",
                    DisplayName = "SUBMT",
                    FullName = "Submit Role"
                };

                userManager.Create(user, "Abc&123!");

                userManager.AddToRoles(user.Id,
                    new string[] {
                        "Submitter"
                    });
            }


            if (!context.Priority.Any(u => u.Name == "High"))
            { context.Priority.Add(new TicketPriority { Name = "High" }); }

            if (!context.Priority.Any(u => u.Name == "Medium"))
            { context.Priority.Add(new TicketPriority { Name = "Medium" }); }

            if (!context.Priority.Any(u => u.Name == "Low"))
            { context.Priority.Add(new TicketPriority { Name = "Low" }); }

            if (!context.Priority.Any(u => u.Name == "Urgent"))
            { context.Priority.Add(new TicketPriority { Name = "Urgent" }); }

            if (!context.Type.Any(u => u.Name == "Production Fix"))
            { context.Type.Add(new TicketType { Name = "Production Fix" }); }

            if (!context.Type.Any(u => u.Name == "Project Task"))
            { context.Type.Add(new TicketType { Name = "Project Task" }); }

            if (!context.Type.Any(u => u.Name == "Software Update"))
            { context.Type.Add(new TicketType { Name = "Software Update" }); }

            if (!context.Status.Any(u => u.Name == "New"))
            { context.Status.Add(new TicketStatus { Name = "New" }); }

            if (!context.Status.Any(u => u.Name == "In Development"))
            { context.Status.Add(new TicketStatus { Name = "In Development" }); }

            if (!context.Status.Any(u => u.Name == "Completed"))
            { context.Status.Add(new TicketStatus { Name = "Completed" }); }


        }
    }
}
