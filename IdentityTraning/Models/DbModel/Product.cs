using System.Collections.Generic;

namespace IdentityTraning.Models.DbModel
{
    public class Product
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Information { get; set; }
        public decimal Cost { get; set; }
        public string ImgLink { get; set; }

        public virtual List<Shop> Shops { get; set; }
        public virtual List<Check> Checks { get; set; }
    }
}