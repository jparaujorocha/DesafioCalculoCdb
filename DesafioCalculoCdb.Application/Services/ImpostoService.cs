using AutoMapper;
using DesafioCalculoCdb.Application.DTOs;
using DesafioCalculoCdb.Application.Interfaces;
using DesafioCalculoCdb.Domain.Entities;
using DesafioCalculoCdb.Domain.Interfaces;
using DesafioCalculoCdb.Shared.Enums;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace DesafioCalculoCdb.Application.Services
{
    public class ImpostoService : IImpostoService
    {
        private IImpostoRepository _impostoRepository;
        private IImpostoInvestimentoService _impostoInvestimentoService;
        private readonly IMapper _mapper;

        public ImpostoService(IImpostoRepository impostoRepository, IImpostoInvestimentoService impostoInvestimentoService, IMapper mapper)
        {
            _impostoRepository = impostoRepository;
            _impostoInvestimentoService = impostoInvestimentoService;
            _mapper = mapper;
        }
        public decimal CalculaImpostoLiquido(int idInvestimento, int prazoResgate)
        {
            switch (idInvestimento)
            {
                case (int)EnumInvestimento.CDB:
                    return CalculaImpostoCdb(prazoResgate);                    
                default:
                    throw new System.Exception("Investimento não disponível ou inexistente.");                       
            }
        }

        [ExcludeFromCodeCoverage]
        private decimal CalculaImpostoCdb(int prazoResgate)
        {
            if (prazoResgate <= 0)
                throw new System.Exception("Prazo de resgate deve ser maior que zero.");

            IEnumerable<ImpostoDTO> listImpostosCdb = GetByIdInvestimento((int)EnumInvestimento.CDB);

            if (listImpostosCdb == null || listImpostosCdb.Count() == 0)
                throw new System.Exception("Impostos do Investimento CDB não encontrados.");

            if (prazoResgate <= 6)
            {
                return listImpostosCdb.Where(a => a.Id == (int)EnumImposto.CDB6).Select(b => b).FirstOrDefault().Valor;
            }
            else if (prazoResgate >= 7 && prazoResgate <= 12)
            {
                return listImpostosCdb.Where(a => a.Id == (int)EnumImposto.CDB12).Select(b => b).FirstOrDefault().Valor;
            }
            else if (prazoResgate >= 13 && prazoResgate <= 24)
            {
                return listImpostosCdb.Where(a => a.Id == (int)EnumImposto.CDB24).Select(b => b).FirstOrDefault().Valor;
            }
            else
            {
                return listImpostosCdb.Where(a => a.Id == (int)EnumImposto.CDB24Plus).Select(b => b).FirstOrDefault().Valor;
            }
        }

        public IEnumerable<ImpostoDTO> GetByIdInvestimento(int idInvestimento)
        {
            var listImpostoInvestimentoDto = _impostoInvestimentoService.GetByIdInvestimento(idInvestimento);

            if (listImpostoInvestimentoDto == null || listImpostoInvestimentoDto.Count() == 0)
                return null;

            var listImpostoInvestimento = _mapper.Map<IEnumerable<ImpostoInvestimento>>(listImpostoInvestimentoDto);

            var listImposto = _impostoRepository.GetImpostosAtivosByIdImposto(listImpostoInvestimento);

            var listImpostoDto = _mapper.Map<IEnumerable<ImpostoDTO>>(listImposto);

            return listImpostoDto;
        }
    }
}
