using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity.EntityFramework;

namespace IdentityTraning.Models
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
    }

    public enum Gendar
    {
        Man,
        Woman
    }
}