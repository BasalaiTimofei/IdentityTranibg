using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace IdentityTraning.Models.DbModel
{
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> userManager,
            string authenticationType)
        {
            ClaimsIdentity userIdentity = await userManager.CreateIdentityAsync(this, authenticationType);
            return userIdentity;
        }
    }
}