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
        private readonly IInvestimentoRepository _investimentoRepository;
        private readonly IImpostoService _impostoService;
        private readonly IMapper _mapper;

        public InvestimentoService(IInvestimentoRepository investimentoRepository, IImpostoService impostoService, IMapper mapper)
        {
            _investimentoRepository = investimentoRepository;
            _impostoService = impostoService;
            _mapper = mapper;
        }

        public async Task<InvestimentoDto> GetById(int id)
        {
            var investimentoEntity = await _investimentoRepository.GetById(id);
            return _mapper.Map<InvestimentoDto>(investimentoEntity);
        }
        public bool VerificaExistenciaDeInvestimento(int idInvestimento)
        {
            return _investimentoRepository.VerificaExistenciaInvestimento(idInvestimento);
        }

        public async Task<IEnumerable<InvestimentoDto>> GetInvestimentosAtivos()
        {
            var listInvestimento = await _investimentoRepository.GetInvestimentosAtivos();
            return _mapper.Map<IEnumerable<InvestimentoDto>>(listInvestimento);
        }

        public async Task CalculaSimulacaoInvestimentos(InvestimentoDto dadosInvestimento)
        {
            if (dadosInvestimento.PrazoResgateAplicacao <= 0)
                throw new Exception("Prazo de resgate deve ser maior que zero.");

            if (dadosInvestimento.ValorInicialInvestimento <= 0)
            {
                Exception exception = new Exception("Valor Inicial do Investimento deve ser maior que zero.");
                throw exception;
            }

            if (!VerificaExistenciaDeInvestimento(dadosInvestimento.Id))
                throw new Exception("Investimento não encontrado no banco de dados.");

            switch (dadosInvestimento.Id)
            {
                case (int)EnumInvestimento.CDB:
                    await CalculaCdb(dadosInvestimento, dadosInvestimento.PrazoResgateAplicacao, dadosInvestimento.ValorInicialInvestimento);
                    break;
            }
        }

        [ExcludeFromCodeCoverage]
        private async Task CalculaCdb(InvestimentoDto investimentoEntity, int prazoResgate, decimal valorInicial)
        {
            investimentoEntity.ListInvestimentoMensalDto = new List<InvestimentoMensalDto>(prazoResgate)
            {
                new InvestimentoMensalDto
                {
                    NumeroMes = 1,
                    ValorInicialMensal = valorInicial,
                    ValorFinalMensal = valorInicial * (1 + ((investimentoEntity.ValorTaxaInvestimento / 100) * (investimentoEntity.ValorTaxaBanco / 100)))
                }
            };

            for (int a = 1; a < prazoResgate; a++)
            {
                investimentoEntity.ListInvestimentoMensalDto.Add(

                new InvestimentoMensalDto
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
