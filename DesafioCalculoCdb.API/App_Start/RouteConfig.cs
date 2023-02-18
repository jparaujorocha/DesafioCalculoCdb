using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace DesafioCalculoCdb.API
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
            name: "DefaultApi",
            url: "v1/{controller}/{id}",
            defaults: new { id = RouteParameter.Optional });


        }
    }
}
