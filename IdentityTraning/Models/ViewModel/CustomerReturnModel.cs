using System.Collections.Generic;

namespace IdentityTraning.Models.ViewModel
{
    public class CustomerReturnModel
    {
        public string Url { get; set; }
        public string Id { get; set; }
        public ShortUserModel User { get; set; }
        
        //Id and Date
        public List<string[]> Check { get; set; }
    }
}