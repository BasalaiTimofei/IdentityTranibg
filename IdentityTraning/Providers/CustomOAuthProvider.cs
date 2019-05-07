using System.Security.Claims;
using System.Threading.Tasks;
using IdentityTraning.Models;
using IdentityTraning.Models.DbModel;
using IdentityTraning.Services;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;

namespace IdentityTraning.Providers
{
    public class CustomOAuthProvider : OAuthAuthorizationServerProvider
    {
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
            return Task.FromResult<object>(null);
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var allowedOrigin = "*";
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] {allowedOrigin});
            var userManager = context.OwinContext.GetUserManager<ApplicationUserManager>();
            ApplicationUser applicationUser = await userManager.FindAsync(context.UserName, context.Password);

            if (applicationUser == null)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }

            if (!applicationUser.EmailConfirmed)
            {
                context.SetError("invalid_grant", "User did not confirm email.");
                return;
            }

            ClaimsIdentity claimsIdentity = await applicationUser.GenerateUserIdentityAsync(userManager, "JWT");
            AuthenticationTicket authenticationTicket = new AuthenticationTicket(claimsIdentity, null);

            context.Validated(authenticationTicket);
        }
    }
}