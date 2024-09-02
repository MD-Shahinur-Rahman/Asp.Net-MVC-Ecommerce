using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;
using System.Threading.Tasks;
using E_commerceProject_1280721.DAL;


namespace E_commerceProject_1280721.DAL
{
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(ApplicationUserManager manager)
        {
            // Generate the user's identity here
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom claims or additional user properties as needed
            return userIdentity;
        }

        // Additional properties and methods as needed
    }
}