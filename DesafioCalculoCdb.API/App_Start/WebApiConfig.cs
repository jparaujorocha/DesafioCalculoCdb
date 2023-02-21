using DesafioCalculoCdb.Api.Helper;
using System;
using System.Web.Http;

namespace DesafioCalculoCdb.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
            
            MapperConfig.RegisterProfiles();
            UnityConfig.RegistraComponentes(config);
            SwaggerConfig.Register(config);

            config.Formatters.JsonFormatter.MediaTypeMappings.Add(new System.Net.Http.Formatting.
                              RequestHeaderMapping("Accept",
                              "text/html",
                              StringComparison.InvariantCultureIgnoreCase,
                              true,
                              "application/json"));

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
