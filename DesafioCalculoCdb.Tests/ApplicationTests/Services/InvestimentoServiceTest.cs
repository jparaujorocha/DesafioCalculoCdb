using AutoMapper;
using DesafioCalculoCdb.Application.DTOs;
using DesafioCalculoCdb.Application.Interfaces;
using DesafioCalculoCdb.Application.Mappings;
using DesafioCalculoCdb.Application.Services;
using DesafioCalculoCdb.Domain.Entities;
using DesafioCalculoCdb.Domain.Interfaces;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DesafioCalculoCdb.Tests.ApplicationTests.Services
{
    public class InvestimentoServiceTest
    {
        private Moq.Mock<IInvestimentoRepository> _mockInvestimentoRepository;
        private Moq.Mock<IImpostoService> _mockImpostoService;
        private IMapper _mockIMapper = CreateMockIMapper();

        private static IMapper CreateMockIMapper()
        {
            //auto mapper configuration
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DomainToDTOMappingProfile());
            });
            return mockMapper.CreateMapper();
        }
        private IEnumerable<Investimento> listInvestimentoMockValido = new List<Investimento>
            {
                 new Investimento("CDB", DateTime.Now, null, 0.9m, 108,true, 1),
                 new Investimento("OUTRO", DateTime.Now, null, 0.1m, 10,false, 2)
            };

        private IEnumerable<Imposto> listImpostoMockValido = new List<Imposto>
            {
                new Imposto("CDB6", 1, 22.5M, true, 1),
                new Imposto("CDB12", 1, 20, true, 2),
                new Imposto("CDB24", 1, 17.5M, true, 3),
                new Imposto("CDB24PLUS", 1, 15, true, 4)
            };

        private Investimento investimentoMockValido = new Investimento("CDB", DateTime.Now, null, 0.9m, 108, true, 1);

        private IEnumerable<InvestimentoDTO> listInvestimentoDtoMockValido;
        private InvestimentoDTO investimentoDtoMockValido;

        private IEnumerable<ImpostoDTO> listImpostoDtoMockValido;


        [Fact]
        public void GetInvestimentosAtivos_SemParametro_RetornaListaObjeto()
        {
            _mockInvestimentoRepository = new Mock<IInvestimentoRepository>();
            _mockImpostoService = new Mock<IImpostoService>();
            listInvestimentoDtoMockValido = _mockIMapper.Map<IEnumerable<InvestimentoDTO>>(listInvestimentoMockValido);


            _mockInvestimentoRepository.Setup(a => a.GetInvestimentosAtivos()).Returns(Task.FromResult(listInvestimentoMockValido));

            IInvestimentoService investimentoService = new InvestimentoService(_mockInvestimentoRepository.Object, _mockImpostoService.Object,
                                                                                                    _mockIMapper);
            var listaInvestimento = investimentoService.GetInvestimentosAtivos();

            listInvestimentoDtoMockValido.Should().BeEquivalentTo(listaInvestimento.Result);
        }
        [Fact]
        public void GetInvestimentosAtivos_SemParametro_RetornaListaObjetoVazia()
        {
            listInvestimentoMockValido = null;
            _mockInvestimentoRepository = new Mock<IInvestimentoRepository>();
            _mockImpostoService = new Mock<IImpostoService>();
            listInvestimentoDtoMockValido = new List<InvestimentoDTO>();

            _mockInvestimentoRepository.Setup(a => a.GetInvestimentosAtivos()).Returns(Task.FromResult(listInvestimentoMockValido));

            IInvestimentoService investimentoService = new InvestimentoService(_mockInvestimentoRepository.Object, _mockImpostoService.Object,
                                                                                                    _mockIMapper);

            var listaInvestimento = investimentoService.GetInvestimentosAtivos();

            listInvestimentoDtoMockValido.Should().BeEquivalentTo(listaInvestimento.Result);
        }

        [Fact]
        public void GetInvestimentoById_IdExistente_RetornaObjeto()
        {
            _mockInvestimentoRepository = new Mock<IInvestimentoRepository>();
            _mockImpostoService = new Mock<IImpostoService>();
            investimentoDtoMockValido = _mockIMapper.Map<InvestimentoDTO>(investimentoMockValido);


            _mockInvestimentoRepository.Setup(a => a.GetById(It.Is<int>(b => b == 1))).Returns(Task.FromResult(investimentoMockValido));

            IInvestimentoService investimentoService = new InvestimentoService(_mockInvestimentoRepository.Object, _mockImpostoService.Object,
                                                                                                    _mockIMapper);

            var investimento = investimentoService.GetById(1);

            investimentoDtoMockValido.Should().BeEquivalentTo(investimento.Result);
        }
        [Fact]
        public void GetImpostoInvestimentoById_IdNaoExistente_RetornaObjetoVazio()
        {
            _mockInvestimentoRepository = new Mock<IInvestimentoRepository>();
            _mockImpostoService = new Mock<IImpostoService>();
            investimentoDtoMockValido = _mockIMapper.Map<InvestimentoDTO>(investimentoMockValido);


            _mockInvestimentoRepository.Setup(a => a.GetById(It.Is<int>(b => b == 1))).Returns(Task.FromResult(investimentoMockValido));

            IInvestimentoService investimentoService = new InvestimentoService(_mockInvestimentoRepository.Object, _mockImpostoService.Object,
                                                                                                    _mockIMapper);

            var investimento = investimentoService.GetById(0);

            investimento.Result.Should().BeNull();
        }

        [Theory]
        [InlineData(1, 2, 100, 22.5, 79.01)]
        [InlineData(1, 6, 1, 22.5, 0.82)]
        [InlineData(1, 7, 10, 20, 8.56)]
        [InlineData(1, 10, 55, 20, 48.47)]
        [InlineData(1, 12, 158213.00, 20, 142148.95)]
        [InlineData(1, 13, 125874.00, 17.5, 117761.26)]
        [InlineData(1, 17, 33, 17.5, 32.09)]
        [InlineData(1, 24, 2000, 17.5, 2081.17)]
        [InlineData(1, 25, 35, 15, 37.89)]
        [InlineData(1, 100, 4, 15, 8.94)]
        public void CalcularImposto_ParametrosValidos_RetornarValorEsperado(int idInvestimento, int prazoResgate, decimal valorInicial, decimal valorImposto, decimal valorEsperadoRetorno)
        {
            _mockInvestimentoRepository = new Mock<IInvestimentoRepository>();
            _mockImpostoService = new Mock<IImpostoService>();
            listInvestimentoDtoMockValido = _mockIMapper.Map<IEnumerable<InvestimentoDTO>>(listInvestimentoMockValido);
            listImpostoDtoMockValido = _mockIMapper.Map<IEnumerable<ImpostoDTO>>(listImpostoMockValido);

            investimentoDtoMockValido = new InvestimentoDTO
            {
                Id = idInvestimento,
                PrazoResgateAplicacao = prazoResgate,
                ValorInicialInvestimento = valorInicial
            };

            _mockInvestimentoRepository.Setup(a => a.GetById(It.Is<int>(b => b == 1))).Returns(Task.FromResult(investimentoMockValido));
            _mockImpostoService.Setup(a => a.CalculaImpostoLiquido(idInvestimento, prazoResgate)).Returns(valorImposto);

            IInvestimentoService investimentoService = new InvestimentoService(_mockInvestimentoRepository.Object, _mockImpostoService.Object,
                                                                                                    _mockIMapper);

            var resultado = investimentoService.CalculaSimulacaoInvestimentos(investimentoDtoMockValido);

            Assert.Equal(valorEsperadoRetorno, Math.Round(resultado.Result.ValorFinalInvestimento, 2));
        }

        [Theory]
        [InlineData(1, 2, 0)]
        [InlineData(2, 100, -200)]
        public void CalcularImposto_ValorInicialMenorIgualZero_RetornarExcecao(int idInvestimento, int prazoResgate, decimal valorInicial)
        {
            _mockImpostoService = new Mock<IImpostoService>();
            _mockInvestimentoRepository = new Mock<IInvestimentoRepository>();
            investimentoDtoMockValido = new InvestimentoDTO
            {
                Id = idInvestimento,
                PrazoResgateAplicacao = prazoResgate,
                ValorInicialInvestimento = valorInicial
            };

            IInvestimentoService investimentoService = new InvestimentoService(_mockInvestimentoRepository.Object, _mockImpostoService.Object,
                                                                                                    _mockIMapper);

            var exception = Assert.ThrowsAsync<Exception>(() => investimentoService.CalculaSimulacaoInvestimentos(investimentoDtoMockValido));

            Assert.Equal("Valor Inicial do Investimento deve ser maior que zero.", exception.Result?.Message);
        }

        [Theory]
        [InlineData(0, 2, 400)]
        [InlineData(500, 100, 1000)]
        public void CalcularImposto_InvestimentoNaoListado_RetornarExcecao(int idInvestimento, int prazoResgate, decimal valorInicial)
        {
            _mockImpostoService = new Mock<IImpostoService>();
            _mockInvestimentoRepository = new Mock<IInvestimentoRepository>();
            investimentoDtoMockValido = new InvestimentoDTO
            {
                Id = idInvestimento,
                PrazoResgateAplicacao = prazoResgate,
                ValorInicialInvestimento = valorInicial
            };

            IInvestimentoService investimentoService = new InvestimentoService(_mockInvestimentoRepository.Object, _mockImpostoService.Object,
                                                                                                    _mockIMapper);

            var exception = Assert.ThrowsAsync<Exception>(() => investimentoService.CalculaSimulacaoInvestimentos(investimentoDtoMockValido));

            Assert.Equal("Investimento não encontrado no banco de dados.", exception.Result?.Message);
        }

        [Theory]
        [InlineData(1, 0, 2)]
        [InlineData(1, -1, 2)]
        public void CalcularSimulacaoInvestimento_PrazoResgateMenorIgualZero_RetornarExcecao(int idInvestimento, int prazoResgate, decimal valorInicial)
        {
            _mockImpostoService = new Mock<IImpostoService>();
            _mockInvestimentoRepository = new Mock<IInvestimentoRepository>();
            investimentoDtoMockValido = new InvestimentoDTO
            {
                Id = idInvestimento,
                PrazoResgateAplicacao = prazoResgate,
                ValorInicialInvestimento = valorInicial
            };

            IInvestimentoService investimentoService = new InvestimentoService(_mockInvestimentoRepository.Object, _mockImpostoService.Object,
                                                                                                    _mockIMapper);

            var exception = Assert.ThrowsAsync<Exception>(() => investimentoService.CalculaSimulacaoInvestimentos(investimentoDtoMockValido));
            Assert.Equal("Prazo de resgate deve ser maior que zero.", exception.Result?.Message);
        }
    }
}
