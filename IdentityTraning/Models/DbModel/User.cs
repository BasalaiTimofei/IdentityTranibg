using System;

namespace IdentityTraning.Models.DbModel
{
    public class User
    {
        public string Id { get; set; }

        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SecondName { get; set; }
        public Gendar Gendar { get; set; }
        public string Location { get; set; }
        public DateTime JoinDate { get; set; }

        public virtual Worker Worker { get; set; }
        public virtual Customer Customer { get; set; }
    }
    public enum Gendar
    {
        Man,
        Woman
    }
}