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

            Uri locationHeader = new Uri(Url.Link("GetUserById", new {id = applicationUser.Id}));
            return Created(locationHeader, ModelFactory.Create(applicationUser));
        }
    }
}