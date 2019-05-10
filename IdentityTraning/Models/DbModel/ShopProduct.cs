namespace IdentityTraning.Models.DbModel
{
    public class ShopProduct
    {
        public string Id { get; set; }

        public int Count { get; set; }
        public decimal Price { get; set; }

        public virtual Shop Shop { get; set; }
        public virtual Product Product { get; set; }
    }
}