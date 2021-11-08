using System.Web.Http;
using WebActivatorEx;
using JWTAuthAPI;
using Swashbuckle.Application;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace JWTAuthAPI
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                    {
                        c.SingleApiVersion("v1", "JWTAuthAPI");
                        c.ApiKey("Authorization")
                            .Description("Put your token here")
                            .Name("Authorization")
                            .In("header");                        
                    })
                .EnableSwaggerUi(c =>
                    {                        
                        c.EnableApiKeySupport("Authorization", "header");
                    });
        }
    }
}
