using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using IdentityTraning.Models;
using IdentityTraning.Models.DbModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using IdentityTraning.Models.Roles;

namespace IdentityTraning.Controllers
{
    [Authorize(Roles = "Admin")]
    [RoutePrefix("api/roles")]
    public class RoleController : BaseApiController
    {
        [Route("id:guid", Name = "GetRoleById")]
        public async Task<IHttpActionResult> GetRole(string id)
        {
            IdentityRole identityRole = await ApplicationRoleManager.FindByIdAsync(id);

            if (identityRole == null) return NotFound();

            return Ok(ModelFactory.Create(identityRole));
        }

        [Route("", Name = "GetAllRoles")]
        public IHttpActionResult GetAllRoles()
        {
            IQueryable<IdentityRole> identityRoles = ApplicationRoleManager.Roles;

            return Ok(identityRoles);
        }

        [Route("create")]
        public async Task<IHttpActionResult> Create(CreateRoleModel createRoleModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            IdentityRole identityRole = new IdentityRole {Name = createRoleModel.Name};
            IdentityResult identityResult = await ApplicationRoleManager.CreateAsync(identityRole);

            if (!identityResult.Succeeded) return GetErrorResult(identityResult);

            Uri locationHeader = new Uri(Url.Link("GetRoleById", new {id = identityRole.Id}));

            return Created(locationHeader, ModelFactory.Create(identityRole));
        }

        [Route("{id:guid}", Name = "DeleteRole")]
        public async Task<IHttpActionResult> DeleteRole(string id)
        {
            IdentityRole identityRole = await ApplicationRoleManager.FindByIdAsync(id);

            if (identityRole == null) NotFound();

            IdentityResult identityResult = await ApplicationRoleManager.DeleteAsync(identityRole);

            if (!identityResult.Succeeded) return GetErrorResult(identityResult);

            return Ok();
        }

        [Route("manageUsersInRole")]
        public async Task<IHttpActionResult> ManageUsersInRole(UserInRoleModel userInRoleModel)
        {
            IdentityRole identityRole = await ApplicationRoleManager.FindByIdAsync(userInRoleModel.Id);

            if (identityRole == null)
            {
                ModelState.AddModelError("", "Role does not exist");
                return BadRequest(ModelState);
            }

            foreach (string userId in userInRoleModel.EnrolledUsers)
            {
                ApplicationUser applicationUser = await ApplicationUserManager.FindByIdAsync(userId);
                
                if (applicationUser == null)
                {
                   ModelState.AddModelError("", $"User by Id: {userId} dous not exist");
                   continue;
                }

                if (!ApplicationUserManager.IsInRole(userId, identityRole.Name))
                {
                    IdentityResult identityResult =
                        await ApplicationUserManager.AddToRoleAsync(userId, identityRole.Name);

                    if (!identityResult.Succeeded)
                        ModelState.AddModelError("", $"User by Id: {userId} could not be added to role");
                }
            }

            foreach (string userId in userInRoleModel.RemovedUsers)
            {
                ApplicationUser applicationUser = await ApplicationUserManager.FindByIdAsync(userId);

                if (applicationUser == null)
                {
                    ModelState.AddModelError("", $"User by Id: {userId} does not exist");
                    continue;
                }

                IdentityResult identityResult =
                    await ApplicationUserManager.RemoveFromRoleAsync(userId, identityRole.Name);

                if (!identityResult.Succeeded)
                    ModelState.AddModelError("", $"User by Id: {userId} could not be removed from role");
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok();
        }
    }
}
