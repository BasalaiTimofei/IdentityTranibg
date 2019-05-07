using IdentityTraning.Models.DbModel;

namespace IdentityTraning.Models
{
    public class ShortUserModel
    {
        public string Id { get; set; }
        public string FullName { get; set; }

        public static ShortUserModel Create(ApplicationUser applicationUser)
        {
            return new ShortUserModel
            {
                Id = applicationUser.Id,
                FullName = $"{applicationUser.LastName} " +
                           $"{applicationUser.FirstName} " +
                           $"{applicationUser.SecondName}"
            };
        }
    }
}