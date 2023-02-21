using AutoMapper;
using DesafioCalculoCdb.Api.Controllers;
using DesafioCalculoCdb.Application.DTOs;
using DesafioCalculoCdb.Application.Interfaces;
using DesafioCalculoCdb.Application.Mappings;
using DesafioCalculoCdb.Domain.Entities;
using DesafioCalculoCdb.Shared.Helpers;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Xunit;

namespace DesafioCalculoCdb.Tests.APITests
{
    public class InvestimentosControllerTests
    {
        private Moq.Mock<IInvestimentoService> _mockInvestimentoService;
        private readonly IMapper _mockIMapper = CreateMockIMapper();

        private static IMapper CreateMockIMapper()
        {
            //auto mapper configuration
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DomainToDtoMappingProfile());
            });
            return mockMapper.CreateMapper();
        }
        private readonly IEnumerable<Investimento> listInvestimentoMockValido = new List<Investimento>
            {
                 new Investimento("CDB", DateTime.Now, null, 0.9m, 108,true, 1),
                 new Investimento("OUTRO", DateTime.Now, null, 0.1m, 10,false, 2)
            };

        private readonly Investimento investimentoMockValido = new Investimento("CDB", DateTime.Now, null, 0.9m, 108, true, 1);

        private IEnumerable<InvestimentoDto> listInvestimentoDtoMockValido;
        private InvestimentoDto investimentoDtoMockValido;

        [Fact]
        public void GetInvestimentosAtivos_SemParametro_RetornaListaObjeto()
        {
            _mockInvestimentoService = new Mock<IInvestimentoService>();
            listInvestimentoDtoMockValido = _mockIMapper.Map<IEnumerable<InvestimentoDto>>(listInvestimentoMockValido.Where(a => a.Ativo));

            _mockInvestimentoService.Setup(a => a.GetInvestimentosAtivos()).Returns(Task.FromResult(listInvestimentoDtoMockValido));

            var investimentosController = new InvestimentosController(_mockInvestimentoService.Object);

            JsonResult jsonActionResult = (JsonResult)investimentosController.GetRecuperarInvestimentosAtivos().Result;

            var resultadoEsperado = jsonActionResult.ExtractType<IEnumerable<InvestimentoDto>>();

            listInvestimentoDtoMockValido.Should().BeEquivalentTo(resultadoEsperado);
        }

        [Fact]
        public void GetInvestimentosAtivos_InvestimentosNaoEncotrados_RetornaExcecao()
        {
            _mockInvestimentoService = new Mock<IInvestimentoService>();
            listInvestimentoDtoMockValido = new List<InvestimentoDto>();

            _mockInvestimentoService.Setup(a => a.GetInvestimentosAtivos()).Returns(Task.FromResult(listInvestimentoDtoMockValido));

            var investimentosController = new InvestimentosController(_mockInvestimentoService.Object);

            var exception = Assert.ThrowsAsync<Exception>(() => investimentosController.GetRecuperarInvestimentosAtivos());

            Assert.Equal("Lista de investimentos indispoível no momento.", exception.Result?.Message);
        }

        [Fact]
        public void PostCalculaInvestimento_IdInvestimentoNaoviado_RetornaExcecao()
        {
            _mockInvestimentoService = new Mock<IInvestimentoService>();
            listInvestimentoDtoMockValido = new List<InvestimentoDto>();
            investimentoDtoMockValido = new InvestimentoDto
            {
                Id = 0
            };

            var investimentosController = new InvestimentosController(_mockInvestimentoService.Object);

            var exception = Assert.ThrowsAsync<Exception>(() => investimentosController.PostCalcularInvestimentos(investimentoDtoMockValido));

            Assert.Equal("É necessário selecionar um investimento!", exception.Result?.Message);
        }

        [Fact]
        public void PostCalculaInvestimento_DadosValidos_RetornaObjetoComSucesso()
        {
            _mockInvestimentoService = new Mock<IInvestimentoService>();
            listInvestimentoDtoMockValido = new List<InvestimentoDto>();
            var respostaInvestimentoDtoMockValido = _mockIMapper.Map<InvestimentoDto>(listInvestimentoMockValido.FirstOrDefault(a => a.Id == 1));

            respostaInvestimentoDtoMockValido.ValorImposto = 22.50m;
            respostaInvestimentoDtoMockValido.ValorFinalInvestimento = 3950.69610380000000m;

            respostaInvestimentoDtoMockValido.ListInvestimentoMensalDto = new List<InvestimentoMensalDto>()
            {
                new InvestimentoMensalDto()
                {
                    NumeroMes = 1,
                    ValorInicialMensal = 5000m,
                    ValorFinalMensal = 5048.600000m
                },
                new InvestimentoMensalDto()
                {
                    NumeroMes = 1,
                    ValorInicialMensal = 5048.600000m,
                    ValorFinalMensal = 5097.67239200000m
                }
            };

            _mockInvestimentoService.Setup(a => a.CalculaSimulacaoInvestimentos(It.IsAny<InvestimentoDto>())).Returns(Task.FromResult(respostaInvestimentoDtoMockValido));
            var investimentosController = new InvestimentosController(_mockInvestimentoService.Object);

            JsonResult jsonActionResult = (JsonResult)investimentosController.PostCalcularInvestimentos(_mockIMapper.Map<InvestimentoDto>(respostaInvestimentoDtoMockValido)).Result;

            var resultadoEsperado = jsonActionResult.ExtractType<InvestimentoDto>();

            respostaInvestimentoDtoMockValido.Should().BeEquivalentTo(resultadoEsperado);
        }
    }
}
