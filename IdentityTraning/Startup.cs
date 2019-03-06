using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using IdentityTraning.Context;
using IdentityTraning.Services;
using Microsoft.Owin.Cors;
using Newtonsoft.Json.Serialization;
using Owin;

namespace IdentityTraning
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            HttpConfiguration httpConfiguration = new HttpConfiguration();
            ConfigureOAuthTokenGeneration(appBuilder);
            ConfigureWebApi(httpConfiguration);
            appBuilder.UseCors(CorsOptions.AllowAll);
            appBuilder.UseWebApi(httpConfiguration);
        }

        private static void ConfigureOAuthTokenGeneration(IAppBuilder appBuilder)
        {
            appBuilder.CreatePerOwinContext(ApplicationContext.Create);
            appBuilder.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
        }

        private static void ConfigureWebApi(HttpConfiguration httpConfiguration)
        {
            httpConfiguration.MapHttpAttributeRoutes();
            JsonMediaTypeFormatter jsonMediaTypeFormatter =
                httpConfiguration.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonMediaTypeFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}