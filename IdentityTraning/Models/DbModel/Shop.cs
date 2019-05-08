using System.Collections.Generic;

namespace IdentityTraning.Models.DbModel
{
    public class Shop
    {
        public string Id { get; set; }

        public string Adress { get; set; }
        public string Information { get; set; }
        public string ImgLink { get; set; }

        public virtual List<Worker> Workers { get; set; }
        public virtual List<Product> Products { get; set; }
        public virtual List<Check> Checks { get; set; }
        public virtual List<Position> Positions { get; set; }
    }
}