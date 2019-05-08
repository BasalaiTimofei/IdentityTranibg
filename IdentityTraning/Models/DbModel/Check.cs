using System;
using System.Collections.Generic;

namespace IdentityTraning.Models.DbModel
{
    public class Check
    {
        public string Id { get; set; }

        public virtual List<Product> Products { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Shop Shop { get; set; }

        public DateTime PurchaseDate { get; set; }
    }
}