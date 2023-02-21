using Newtonsoft.Json;
using System.Web.Mvc;

namespace DesafioCalculoCdb.Shared.Helpers
{
    public static class Extensions
    {
        public static T ExtractType<T>(this JsonResult result)
        {
            var resultAsJson = JsonConvert.SerializeObject(result.Data);
            return JsonConvert.DeserializeObject<T>(resultAsJson);
        }
    }
}
