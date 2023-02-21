using System.Web.Http;
using Swashbuckle.Application;

namespace DesafioCalculoCdb.Api
{
    public class SwaggerConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            config
                  .EnableSwagger(c =>
                  {
                      c.SingleApiVersion("v1", "HomeShare.API");
                      c.IncludeXmlComments(GetXmlCommentsPath());
                  })
                  .EnableSwaggerUi(c =>
                  {

                  });
        }
        protected static string GetXmlCommentsPath()
        {
            return System.String.Format(@"{0}\bin\HomeShare.API.XML", System.AppDomain.CurrentDomain.BaseDirectory);
        }
    }
}
