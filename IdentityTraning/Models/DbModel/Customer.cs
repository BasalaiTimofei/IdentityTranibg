using System.Collections.Generic;

namespace IdentityTraning.Models.DbModel
{
    public class Customer
    {
        public string Id { get; set; }

        public virtual User User { get; set; }
        //public virtual List<Check> Checks { get; set; }
    }
}