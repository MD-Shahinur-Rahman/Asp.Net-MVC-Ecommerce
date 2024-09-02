namespace E_commerceProject_1280721.Migrations
{
    using E_commerceProject_1280721.DAL;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<E_commerceProject_1280721.DAL.EcommerceContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(E_commerceProject_1280721.DAL.EcommerceContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

         
            if (!roleManager.RoleExists("Admin"))
            {
                roleManager.Create(new IdentityRole("Admin"));
            }

            if (!roleManager.RoleExists("Customer"))
            {
                roleManager.Create(new IdentityRole("Customer"));
            }

    
            if (userManager.FindByName("admin@gmail.com") == null)
            {
                var adminUser = new ApplicationUser { UserName = "admin@gmail.com", Email = "admin@gmail.com" };
                var result = userManager.Create(adminUser, "123456");

                if (result.Succeeded)
                {
                    userManager.AddToRole(adminUser.Id, "Admin");
                }
                else
                {
                  
                    foreach (var error in result.Errors)
                    {
                       
                    }
                }
            }

            if (userManager.FindByName("customer@gmail.com") == null)
            {
                var customerUser = new ApplicationUser { UserName = "customer@gmail.com", Email = "customer@gmail.com" };
                var result = userManager.Create(customerUser, "12345");

                if (result.Succeeded)
                {
                    userManager.AddToRole(customerUser.Id, "Customer");
                }
                else
                {
                
                    foreach (var error in result.Errors)
                    {
                       
                    }
                }

            }
        }
    }
}
