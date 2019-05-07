using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace IdentityTraning.Models.DbModel
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        [Required]
        [MaxLength(50)]
        public string SecondName { get; set; }
        [Required]
        public Gendar Gendar { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public DateTime JoinDate { get; set; }

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