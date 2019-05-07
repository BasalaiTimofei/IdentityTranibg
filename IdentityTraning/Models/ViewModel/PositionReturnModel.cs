using System.Collections.Generic;

namespace IdentityTraning.Models.ViewModel
{
    public class PositionReturnModel
    {
        public string Url { get; set; }
        public string Id { get; set; }
        public string PositionName { get; set; }
        public string Duties { get; set; }
        public List<ShortUserModel> Workers { get; set; }
    }
}