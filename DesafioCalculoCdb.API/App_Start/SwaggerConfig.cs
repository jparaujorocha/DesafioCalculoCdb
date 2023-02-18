using System.Web.Http;
using WebActivatorEx;
using DesafioCalculoCdb.API;
using Swashbuckle.Application;

namespace DesafioCalculoCdb.API
{
    /// <summary>
    /// 
    /// </summary>
    public class SwaggerConfig
    {
        /// <summary>
        /// 
        /// </summary>
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;
            GlobalConfiguration.Configuration
              .EnableSwagger()
              .EnableSwaggerUi();
        }
    }
}
