using AutoMapper;
using DesafioCalculoCdb.Application.DTOs;
using DesafioCalculoCdb.Domain.Entities;

namespace DesafioCalculoCdb.Application.Mappings
{
    public class DomainToDTOMappingProfile : Profile
    {
        public DomainToDTOMappingProfile()
        {
            CreateMap<Imposto, ImpostoDTO>().ReverseMap();
            CreateMap<ImpostoInvestimento, ImpostoInvestimentoDTO>().ReverseMap();
            CreateMap<Investimento, InvestimentoDTO>().ReverseMap();
        }
    }
}
