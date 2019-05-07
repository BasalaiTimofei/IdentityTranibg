using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using IdentityTraning.Models;
using IdentityTraning.Models.DbModel;
using Microsoft.AspNet.Identity;

namespace IdentityTraning.Controllers
{
    [RoutePrefix("api/accounts")]
    public class AccountsController : BaseApiController
    {
        [Authorize]
        [Route("users")]
        public IHttpActionResult GetUsers()
        {
            return Ok(ApplicationUserManager.Users.ToListAsync().Result.Select(s => ModelFactory.Create(s)));
        }

        [Authorize]
        [Route("user/{id:guid}", Name = "GetUserById")]
        public async Task<IHttpActionResult> GetUserById(string id)
        {
            ApplicationUser applicationUser = await ApplicationUserManager.FindByIdAsync(id);
            if (applicationUser == null) return NotFound();
            return Ok(ModelFactory.Create(applicationUser));
        }

        [Authorize]
        [Route("user/username", Name = "GetUserByName")]
        public async Task<IHttpActionResult> GetUserByName(string username)
        {
            ApplicationUser applicationUser = await ApplicationUserManager.FindByNameAsync(username);
            if (applicationUser == null) return NotFound();
            return Ok(ModelFactory.Create(applicationUser));
        }

        [AllowAnonymous]
        [Route("create")]
        public async Task<IHttpActionResult> CreateUser(UserRegisrtationModel userRegisrtationModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            ApplicationUser applicationUser = new ApplicationUser
            {
                UserName = userRegisrtationModel.Email,
                Email = userRegisrtationModel.Email,
                FirstName = userRegisrtationModel.FirstName,
                LastName = userRegisrtationModel.LastName,
                SecondName = userRegisrtationModel.SecondName,
                Location = "Belarus Minsk", //TODO добавить поиск по ip
                Gendar = userRegisrtationModel.Gendar ? Gendar.Man : Gendar.Woman,
                JoinDate = DateTime.Now,
                PhoneNumber = userRegisrtationModel.PhoneNumber
            };

            IdentityResult identityResult =
                await ApplicationUserManager.CreateAsync(applicationUser, userRegisrtationModel.Password);

            if (!identityResult.Succeeded) return GetErrorResult(identityResult);

            string code = await ApplicationUserManager.GenerateEmailConfirmationTokenAsync(applicationUser.Id);
            Uri callbackUrl = new Uri(Url.Link("ConfirmEmailRoute", new {userId = applicationUser.Id, code = code}));
            await ApplicationUserManager.SendEmailAsync(applicationUser.Id, "Confirm your account",
                $"Please confirm your account by clicking <a href=\"{callbackUrl}\">here</a>");

        Uri locationHeader = new Uri(Url.Link("GetUserById", new {id = applicationUser.Id}));
            return Created(locationHeader, ModelFactory.Create(applicationUser));
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("ConfirmEmail", Name = "ConfirmEmailRoute")]
        public async Task<IHttpActionResult> ConfirmEmail(string userId, string code)
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(code))
            {
                ModelState.AddModelError("", "User Id and Code are required");
                return BadRequest(ModelState);
            }

            IdentityResult identityResult = await ApplicationUserManager.ConfirmEmailAsync(userId, code);

            return identityResult.Succeeded ? Ok() : GetErrorResult(identityResult);
        }

        [Authorize]
        [HttpPost]
        [Route("ChangePassword")]
        public async Task<IHttpActionResult> CgangePassword(ChangePasswordModel changePasswordModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            IdentityResult identityResult = await ApplicationUserManager.ChangePasswordAsync(User.Identity.GetUserId(),
                changePasswordModel.OldPassword, changePasswordModel.NewPassword);

            if (!identityResult.Succeeded) return GetErrorResult(identityResult);

            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("user/{id:guid}")]
        public async Task<IHttpActionResult> DeleteUser(string id)
        {
            ApplicationUser applicationUser = await ApplicationUserManager.FindByIdAsync(id);

            if (applicationUser == null) return NotFound();

            IdentityResult identityResult = await ApplicationUserManager.DeleteAsync(applicationUser);

            if (!identityResult.Succeeded) return GetErrorResult(identityResult);

            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [Route("user/{id:guid}/roles")]
        [HttpPut]
        public async Task<IHttpActionResult> AssignRolesToUser([FromUri] string id, [FromBody] string[] rolesToAssign)
        {
            ApplicationUser applicationUser = await ApplicationUserManager.FindByIdAsync(id);

            if (applicationUser == null) return NotFound();

            IList<string> currentRoles = await ApplicationUserManager.GetRolesAsync(applicationUser.Id);

            string[] rolesNotExist = rolesToAssign.Except(ApplicationRoleManager.Roles.Select(s => s.Name)).ToArray();

            if (rolesNotExist.Length > 0)
            {
                ModelState.AddModelError("",
                    $"Roles '{string.Join(",", rolesNotExist)}' does not exists in the system");
                return BadRequest(ModelState);
            }

            IdentityResult removedResult =
                await ApplicationUserManager.RemoveFromRolesAsync(applicationUser.Id, currentRoles.ToArray());

            if (!removedResult.Succeeded)
            {
                ModelState.AddModelError("", "Failed to remove user roles");
                return BadRequest(ModelState);
            }

            IdentityResult addResult = await ApplicationUserManager.AddToRolesAsync(applicationUser.Id, rolesToAssign);

            if (!addResult.Succeeded)
            {
                ModelState.AddModelError("", "Failed to add user roles");
                return BadRequest(ModelState);
            }

            return Ok();
        }
    }
}