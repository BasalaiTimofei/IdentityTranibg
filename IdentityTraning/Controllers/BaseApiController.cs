using System.Linq;
using System.Net.Http;
using System.Web.Http;
using IdentityTraning.Models;
using IdentityTraning.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace IdentityTraning.Controllers
{
    public class BaseApiController : ApiController
    {
        private ModelFactory _modelFactory;
        private ApplicationUserManager _applicationUserManager;
        private ApplicationRoleManager _applicationRoleManager;

        protected ApplicationUserManager ApplicationUserManager =>
            _applicationUserManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();

        protected ApplicationRoleManager ApplicationRoleManager =>
            _applicationRoleManager ?? Request.GetOwinContext().GetUserManager<ApplicationRoleManager>();

        protected ModelFactory ModelFactory =>
            _modelFactory ?? (_modelFactory = new ModelFactory(Request, ApplicationUserManager));

        protected IHttpActionResult GetErrorResult(IdentityResult identityResult)
        {
            if (identityResult == null)
                return InternalServerError();

            if (identityResult.Succeeded)
                return null;

            if (ModelState.IsValid)
                return BadRequest();

            if (identityResult.Errors.Any())
                foreach (var identityResultError in identityResult.Errors)
                    ModelState.AddModelError("", identityResultError);
            return BadRequest(ModelState);
        }
    }
}