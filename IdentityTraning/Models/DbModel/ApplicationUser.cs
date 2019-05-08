using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace IdentityTraning.Models.DbModel
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SecondName { get; set; }
        public Gendar Gendar { get; set; }
        public string Location { get; set; }
        public DateTime JoinDate { get; set; }

        public virtual Worker Worker { get; set; }
        public virtual Customer Customer { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> userManager,
            string authenticationType)
        {
            ClaimsIdentity userIdentity = await userManager.CreateIdentityAsync(this, authenticationType);
            return userIdentity;
        }

    }

    public enum Gendar
    {
        Man,
        Woman
    }
}