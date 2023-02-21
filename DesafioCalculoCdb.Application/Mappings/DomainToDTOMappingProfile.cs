using AutoMapper;
using DesafioCalculoCdb.Application.DTOs;
using DesafioCalculoCdb.Domain.Entities;

namespace DesafioCalculoCdb.Application.Mappings
{
    public class DomainToDtoMappingProfile : Profile
    {
        public DomainToDtoMappingProfile()
        {
            CreateMap<Imposto, ImpostoDto>().ReverseMap();
            CreateMap<ImpostoInvestimento, ImpostoInvestimentoDto>().ReverseMap();
            CreateMap<Investimento, InvestimentoDto>().ReverseMap();
        }
    }
}
