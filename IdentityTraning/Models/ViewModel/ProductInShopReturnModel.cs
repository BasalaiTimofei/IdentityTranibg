using System.Collections.Generic;

namespace IdentityTraning.Models.ViewModel
{
    public class ProductInShopReturnModel
    {
        public string Url { get; set; }
        public string Id { get; set; }
        public string ProductName { get; set; }
        
        // Id and Adress
        public string[] Shop { get; set; }
        public string Information { get; set; }
        public string ImgLink { get; set; }
        public int Count { get; set; }
        public string Price { get; set; }
    }
}