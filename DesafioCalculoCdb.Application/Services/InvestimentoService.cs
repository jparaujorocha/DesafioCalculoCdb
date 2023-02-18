using AutoMapper;
using DesafioCalculoCdb.Application.DTOs;
using DesafioCalculoCdb.Application.Interfaces;
using DesafioCalculoCdb.Domain.Interfaces;
using DesafioCalculoCdb.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioCalculoCdb.Application.Services
{
    public class InvestimentoService : IInvestimentoService
    {
        private IInvestimentoRepository _investimentoRepository;
        private IImpostoService _impostoService;
        private readonly IMapper _mapper;

        public InvestimentoService(IInvestimentoRepository investimentoRepository, IImpostoService impostoService, IMapper mapper)
        {
            _investimentoRepository = investimentoRepository;
            _impostoService = impostoService;
            _mapper = mapper;
        }

        public async Task<InvestimentoDTO> GetById(int id)
        {
            var investimentoEntity = await _investimentoRepository.GetById(id);
            return _mapper.Map<InvestimentoDTO>(investimentoEntity);
        }

        public async Task<IEnumerable<InvestimentoDTO>> GetInvestimentosAtivos()
        {
            var listInvestimento = await _investimentoRepository.GetInvestimentosAtivos();
            return _mapper.Map<IEnumerable<InvestimentoDTO>>(listInvestimento);
        }

        public async Task<InvestimentoDTO> CalculaSimulacaoInvestimentos(InvestimentoDTO dadosInvestimento)
        {
            if (dadosInvestimento.PrazoResgateAplicacao <= 0)
                throw new Exception("Prazo de resgate deve ser maior que zero.");
            if (dadosInvestimento.ValorInicialInvestimento <= 0)
                throw new Exception("Valor Inicial do Investimento deve ser maior que zero.");

            var investimentoEntity = await GetById(dadosInvestimento.Id);

            if (investimentoEntity == null || investimentoEntity.Id == 0)
                throw new Exception("Investimento não encontrado no banco de dados.");

            switch (investimentoEntity.Id)
            {
                case (int)EnumInvestimento.CDB:
                    CalculaCdb(investimentoEntity, dadosInvestimento.PrazoResgateAplicacao, dadosInvestimento.ValorInicialInvestimento);
                    break;
            }
            return investimentoEntity;
        }

        [ExcludeFromCodeCoverage]
        private void CalculaCdb(InvestimentoDTO investimentoEntity, int prazoResgate, decimal valorInicial)
        {
            investimentoEntity.ListInvestimentoMensalDto = new List<InvestimentoMensalDTO>(prazoResgate)
            {
                new InvestimentoMensalDTO
                {
                    NumeroMes = 1,
                    ValorInicialMensal = valorInicial,
                    ValorFinalMensal = valorInicial * (1 + ((investimentoEntity.ValorTaxaInvestimento / 100) * (investimentoEntity.ValorTaxaBanco / 100)))
                }
            };

            for (int a = 1; a < prazoResgate; a++)
            {
                investimentoEntity.ListInvestimentoMensalDto.Add(

                new InvestimentoMensalDTO
                {
                    NumeroMes = a + 1,
                    ValorInicialMensal = investimentoEntity.ListInvestimentoMensalDto[a - 1].ValorFinalMensal,
                    ValorFinalMensal = (investimentoEntity.ListInvestimentoMensalDto[a - 1].ValorFinalMensal *
                                       (1 + ((investimentoEntity.ValorTaxaInvestimento / 100) * (investimentoEntity.ValorTaxaBanco / 100))))
                });
            }

            investimentoEntity.ValorImposto = _impostoService.CalculaImpostoLiquido(investimentoEntity.Id, prazoResgate);

            investimentoEntity.ValorFinalInvestimento = investimentoEntity.ListInvestimentoMensalDto.Last().ValorFinalMensal -
                                                        (investimentoEntity.ListInvestimentoMensalDto.Last().ValorFinalMensal *
                                                        (investimentoEntity.ValorImposto / 100));
        }

    }
}
