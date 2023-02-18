using System.Web.Http;

namespace DesafioCalculoCdb.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Configuração e serviços de API Web

            // Rotas de API Web
            config.MapHttpAttributeRoutes();
        }
    }
}
