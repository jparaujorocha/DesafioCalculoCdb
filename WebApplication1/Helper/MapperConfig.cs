using AutoMapper;
using DesafioCalculoCdb.Application.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Helper
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