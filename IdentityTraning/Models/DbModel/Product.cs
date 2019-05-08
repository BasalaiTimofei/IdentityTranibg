using System.Collections.Generic;

namespace IdentityTraning.Models.DbModel
{
    public class Product
    {
        public string Id { get; set; }

        public string Name { get; set; }
        public string Information { get; set; }
        public string ImgLink { get; set; }

        public virtual List<ShopProduct> ShopProducts { get; set; }
        public virtual List<Check> Checks { get; set; }
    }
}