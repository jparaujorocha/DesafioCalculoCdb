using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebApplication1.Helper;

namespace WebApplication1
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Configuração e serviços de API Web

            // Rotas de API Web
            config.MapHttpAttributeRoutes();

            MapperConfig.RegisterProfiles();
            UnityConfig.RegistraComponentes(config);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
