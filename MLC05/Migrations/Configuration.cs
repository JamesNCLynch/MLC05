using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MLC05.Helpers;
using MLC05.Models;
using WebGrease.Css.Extensions;

namespace MLC05.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "MLC05.Models.ApplicationDbContext";
        }

        protected override void Seed(ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
//            context.Configuration.LazyLoadingEnabled = true;

            SeedRolesAndUsers();
            SeedClassTypes();
        }

        private void SeedClassTypes()
        {
            var context = new ApplicationDbContext();

            var sct = new List<ScheduledClassType>
            {
                new ScheduledClassType()
                {
                    Id = Guid.NewGuid().ToString(),
                    IsActive = true,
                    Name = "Spinning"
                },
                new ScheduledClassType()
                {
                    Id = Guid.NewGuid().ToString(),
                    IsActive = true,
                    Name = "Bodypump"
                },
                new ScheduledClassType()
                {
                    Id = Guid.NewGuid().ToString(),
                    IsActive = false,
                    Name = "Tums n bums"
                }
            }.ToArray();

            context.ScheduledClassTypes.AddOrUpdate(sct);
        }

        private void SeedRolesAndUsers()
        {
            var context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (!roleManager.RoleExists(RoleNameHelper.AdminName))
            {
                var role = new IdentityRole
                {
                    Name = RoleNameHelper.AdminName
                };
                roleManager.Create(role);

                //Here we create a Admin super user who will maintain the website
                var user = new ApplicationUser
                {
                    UserName = "admin",
                    Email = "jamesbernardlynch@gmail.com"
                };

                string userPWD = "Ballygowan1!";

                var chkUser = userManager.Create(user, userPWD);

                //Add default User to Role Admin   
                if (chkUser.Succeeded)
                {
                    var result1 = userManager.AddToRole(user.Id, RoleNameHelper.AdminName);

                }
            }

            if (!roleManager.RoleExists(RoleNameHelper.AttendeeName))
            {
                var role = new IdentityRole
                {
                    Name = RoleNameHelper.AttendeeName
                };
                roleManager.Create(role);

                var user = new ApplicationUser
                {
                    UserName = "attendee",
                    Email = "corpsemuncher@gmail.com"
                };

                string userPWD = "Ballygowan1!";

                var chkUser = userManager.Create(user, userPWD);

                if (chkUser.Succeeded)
                {
                    var result1 = userManager.AddToRole(user.Id, RoleNameHelper.AttendeeName);
                }
            }

            if (!roleManager.RoleExists(RoleNameHelper.InstructorName))
            {
                var role = new IdentityRole
                {
                    Name = RoleNameHelper.InstructorName
                };
                roleManager.Create(role);

                var user = new ApplicationUser
                {
                    UserName = "instructor",
                    Email = "jlynch@actionpoint.ie"
                };

                string userPWD = "Ballygowan1!";

                var chkUser = userManager.Create(user, userPWD);

                if (chkUser.Succeeded)
                {
                    var result1 = userManager.AddToRole(user.Id, RoleNameHelper.InstructorName);
                }
            }
        }
    }
}
