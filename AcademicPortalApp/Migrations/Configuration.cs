namespace AcademicPortalApp.Migrations
{
    using AcademicPortalApp.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AcademicPortalApp.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected async override void Seed(AcademicPortalApp.Models.ApplicationDbContext _context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            var store = new UserStore<ApplicationUser>(_context);
            var manager = new UserManager<ApplicationUser>(store);
            var admin = new Admin() { UserName = "admin@admin.com", Email = "admin@admin.com" };
            await manager.CreateAsync(admin, "admin");
            manager.AddToRole(admin.Id, "Admin");
            base.Seed(_context);
        }
    }
}
