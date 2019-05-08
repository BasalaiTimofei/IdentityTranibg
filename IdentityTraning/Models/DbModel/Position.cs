using System.Collections.Generic;

namespace IdentityTraning.Models.DbModel
{
    public class Position
    {
        public string Id { get; set; }

        public string Name { get; set; }
        public string Duties { get; set; }

        public virtual List<Worker> Workers { get; set; }
        public virtual List<Shop> Shops { get; set; }
    }
}