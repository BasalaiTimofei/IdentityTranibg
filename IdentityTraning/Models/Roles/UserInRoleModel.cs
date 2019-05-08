using System.Collections.Generic;

namespace IdentityTraning.Models.Roles
{
    public class UserInRoleModel
    {
        public string Id { get; set; }

        public List<string> EnrolledUsers { get; set; }
        public List<string> RemovedUsers { get; set; }
    }
}