using System;
using System.Collections.Generic;

namespace IdentityTraning.Models.ViewModel
{
    public class CheckReturnModel
    {
        public string Url { get; set; }
        public string Id { get; set; }

        //Id, Name and price
        public List<string[]> Products { get; set; }
        public string FullPrice { get; set; }
        public ShortUserModel Customer { get; set; }
        public string Adress { get; set; }
        public DateTime PurchaseDate { get; set; }
    }
}