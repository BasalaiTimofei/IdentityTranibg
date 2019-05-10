using IdentityTraning.Models.DbModel;

namespace IdentityTraning.Models
{
    public class ShortUserModel
    {
        public string Id { get; set; }
        public string FullName { get; set; }

        public static ShortUserModel Create(User user)
        {
            return new ShortUserModel
            {
                Id = user.Id,
                FullName = $"{user.LastName} " +
                           $"{user.FirstName} " +
                           $"{user.SecondName}"
            };
        }
    }
}