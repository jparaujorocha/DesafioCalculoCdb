using DesafioCalculoCdb.Api.Helper;
using System.Web.Http;

namespace DesafioCalculoCdb.Api
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
