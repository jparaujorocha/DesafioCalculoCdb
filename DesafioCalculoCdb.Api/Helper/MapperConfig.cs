using AutoMapper;
using DesafioCalculoCdb.Application.Mappings;

namespace DesafioCalculoCdb.Api.Helper
{
    public class MapperConfig
    {
        public static IMapper Mapper { get; set; }
        public static void RegisterProfiles()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DomainToDTOMappingProfile());
            });
            Mapper = config.CreateMapper();
        }
    }
}