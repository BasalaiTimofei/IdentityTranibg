using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using IdentityTraning.Models;
using Microsoft.AspNet.Identity;

namespace IdentityTraning.Controllers
{
    [RoutePrefix("api/accounts")]
    public class AccountsController : BaseApiController
    {
        [Route("users")]
        public IHttpActionResult GetUsers()
        {
            return Ok(ApplicationUserManager.Users.ToListAsync().Result.Select(s => ModelFactory.Create(s)));
        }

        [Route("user/{id:guid}", Name = "GetUserById")]
        public async Task<IHttpActionResult> GetUserById(string id)
        {
            ApplicationUser applicationUser = await ApplicationUserManager.FindByIdAsync(id);
            if (applicationUser == null) return NotFound();
            return Ok(ModelFactory.Create(applicationUser));
        }

        [Route("user/username", Name = "GetUserByName")]
        public async Task<IHttpActionResult> GetUserByName(string username)
        {
            ApplicationUser applicationUser = await ApplicationUserManager.FindByNameAsync(username);
            if (applicationUser == null) return NotFound();
            return Ok(ModelFactory.Create(applicationUser));
        }

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
    }
}