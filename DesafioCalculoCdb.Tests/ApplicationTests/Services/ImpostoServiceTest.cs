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
using Xunit;

namespace DesafioCalculoCdb.Tests.ApplicationTests.Services
{
    public class ImpostoServiceTest
    {
        private Moq.Mock<IImpostoRepository> _mockImpostoRepository;
        private Moq.Mock<IImpostoInvestimentoService> _mockImpostoInvestimentoService;
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
        private IEnumerable<ImpostoInvestimento> listImpostoInvestimentoMockValido = new List<ImpostoInvestimento>
            {
                new ImpostoInvestimento(1, 1, DateTime.Now, null, true, 1 ),
                new ImpostoInvestimento(2, 1, DateTime.Now,null,true,2),
                new ImpostoInvestimento(3, 1, DateTime.Now,null,true,3),
                new ImpostoInvestimento(4, 1, DateTime.Now,null,true,4),
                new ImpostoInvestimento(1, 2, DateTime.Now, null, true, 5)
            };

        private Imposto impostoMockValido = new Imposto("CDB6", 1, 22.5M, true, 1);

        private IEnumerable<Imposto> listImpostoMockValido = new List<Imposto>
            {
                new Imposto("CDB6", 1, 22.5M, true, 1),
                new Imposto("CDB12", 1, 20, true, 2),
                new Imposto("CDB24", 1, 17.5M, true, 3),
                new Imposto("CDB24PLUS", 1, 15, true, 4)
            };

        private IEnumerable<ImpostoDTO> listImpostoDtoMockValido;

        [Fact]
        public void GetImpostoByIdInvestimento_IdInvestimentoExistente_RetornaListaObjeto()
        {
            _mockImpostoInvestimentoService = new Mock<IImpostoInvestimentoService>();
            _mockImpostoRepository = new Mock<IImpostoRepository>();
            listImpostoDtoMockValido = _mockIMapper.Map<IEnumerable<ImpostoDTO>>(listImpostoMockValido);
            var listImpostoInvestimentoDtoMockValido = _mockIMapper.Map<IEnumerable<ImpostoInvestimentoDTO>>(listImpostoInvestimentoMockValido);
            listImpostoInvestimentoDtoMockValido = listImpostoInvestimentoDtoMockValido.Where(a => a.IdInvestimento == 1).Select(b => b);

            _mockImpostoInvestimentoService.Setup(a => a.GetByIdInvestimento(It.Is<int>(b => b == 1))).Returns(listImpostoInvestimentoDtoMockValido);
            _mockImpostoRepository.Setup(a => a.GetImpostosAtivosByIdImposto(It.IsAny<IEnumerable<ImpostoInvestimento>>()))
                                                                             .Returns(listImpostoMockValido.Where(a => listImpostoInvestimentoDtoMockValido
                                                                             .Any(b => b.IdImposto == a.Id)).Select(c => c));

            IImpostoService impostoService = new ImpostoService(_mockImpostoRepository.Object, _mockImpostoInvestimentoService.Object, _mockIMapper);

            var listaImpostoInvestimento = impostoService.GetByIdInvestimento(1);

            listImpostoDtoMockValido.Should().BeEquivalentTo(listaImpostoInvestimento);
        }

        [Fact]
        public void GetImpostoByIdInvestimento_IdNaoExistente_RetornaListaNula()
        {
            listImpostoMockValido = new List<Imposto>();
            _mockImpostoInvestimentoService = new Mock<IImpostoInvestimentoService>();
            _mockImpostoRepository = new Mock<IImpostoRepository>();
            listImpostoDtoMockValido = null;
            var listImpostoInvestimentoDtoMockValido = _mockIMapper.Map<IEnumerable<ImpostoInvestimentoDTO>>(listImpostoInvestimentoMockValido);
            listImpostoInvestimentoDtoMockValido = listImpostoInvestimentoDtoMockValido.Where(a => a.IdInvestimento == 1).Select(b => b);

            _mockImpostoInvestimentoService.Setup(a => a.GetByIdInvestimento(It.Is<int>(b => b == 1))).Returns(listImpostoInvestimentoDtoMockValido);
            _mockImpostoRepository.Setup(a => a.GetImpostosAtivosByIdImposto(It.IsAny<IEnumerable<ImpostoInvestimento>>())).Returns(listImpostoMockValido);

            IImpostoService impostoService = new ImpostoService(_mockImpostoRepository.Object, _mockImpostoInvestimentoService.Object, _mockIMapper);

            var listaImpostoInvestimento = impostoService.GetByIdInvestimento(8);

            listImpostoDtoMockValido.Should().BeEquivalentTo(listaImpostoInvestimento);
        }


        [Theory]
        [InlineData(1, 2, 22.5)]
        [InlineData(1, 6, 22.5)]
        [InlineData(1, 7, 20)]
        [InlineData(1, 10, 20)]
        [InlineData(1, 12, 20)]
        [InlineData(1, 13, 17.5)]
        [InlineData(1, 17, 17.5)]
        [InlineData(1, 24, 17.5)]
        [InlineData(1, 25, 15)]
        [InlineData(1, 100, 15)]
        public void CalcularImposto_ParametrosValidos_RetornarValorEsperado(int idInvestimento, int prazoResgate, decimal valorEsperadoRetorno)
        {
            _mockImpostoInvestimentoService = new Mock<IImpostoInvestimentoService>();
            _mockImpostoRepository = new Mock<IImpostoRepository>();
            var listImpostoInvestimentoDtoMockValido = _mockIMapper.Map<IEnumerable<ImpostoInvestimentoDTO>>(listImpostoInvestimentoMockValido);
            _mockImpostoInvestimentoService.Setup(a => a.GetByIdInvestimento(It.Is<int>(b => b == 1))).Returns(listImpostoInvestimentoDtoMockValido);

            _mockImpostoRepository.Setup(a => a.GetImpostosAtivosByIdImposto(It.IsAny<IEnumerable<ImpostoInvestimento>>()))
                                                                             .Returns(listImpostoMockValido.Where(a => listImpostoInvestimentoDtoMockValido
                                                                             .Any(b => b.IdImposto == a.Id)).Select(c => c));

            IImpostoService impostoService = new ImpostoService(_mockImpostoRepository.Object, _mockImpostoInvestimentoService.Object, _mockIMapper);

            var resultado = impostoService.CalculaImpostoLiquido(idInvestimento, prazoResgate);

            Assert.Equal(valorEsperadoRetorno, resultado);
        }

        [Theory]
        [InlineData(1, 2)]
        [InlineData(1, 100)]
        public void CalcularImposto_ImpostoDoInvestimentoNaoEncontado_RetornarExcecao(int idInvestimento, int prazoResgate)
        {
            _mockImpostoInvestimentoService = new Mock<IImpostoInvestimentoService>();
            _mockImpostoRepository = new Mock<IImpostoRepository>();
            var listImpostoInvestimentoDtoMockValido = _mockIMapper.Map<IEnumerable<ImpostoInvestimentoDTO>>(listImpostoInvestimentoMockValido);
            _mockImpostoInvestimentoService.Setup(a => a.GetByIdInvestimento(It.Is<int>(b => b == 1))).Returns(listImpostoInvestimentoDtoMockValido);

            IImpostoService impostoService = new ImpostoService(_mockImpostoRepository.Object, _mockImpostoInvestimentoService.Object, _mockIMapper);

            var exception = Assert.Throws<Exception>(() => impostoService.CalculaImpostoLiquido(idInvestimento, prazoResgate));
            Assert.Equal("Impostos do Investimento CDB não encontrados.", exception.Message);
        }

        [Theory]
        [InlineData(0, 2)]
        [InlineData(100, 100)]
        public void CalcularImposto_InvestimentoNaoListado_RetornarExcecao(int idInvestimento, int prazoResgate)
        {
            _mockImpostoInvestimentoService = new Mock<IImpostoInvestimentoService>();
            _mockImpostoRepository = new Mock<IImpostoRepository>();
            var listImpostoInvestimentoDtoMockValido = _mockIMapper.Map<IEnumerable<ImpostoInvestimentoDTO>>(listImpostoInvestimentoMockValido);
            _mockImpostoInvestimentoService.Setup(a => a.GetByIdInvestimento(It.Is<int>(b => b == 1))).Returns(listImpostoInvestimentoDtoMockValido);


            IImpostoService impostoService = new ImpostoService(_mockImpostoRepository.Object, _mockImpostoInvestimentoService.Object, _mockIMapper);

            var exception = Assert.Throws<Exception>(() => impostoService.CalculaImpostoLiquido(idInvestimento, prazoResgate));
            Assert.Equal("Investimento não disponível ou inexistente.", exception.Message);
        }

        [Theory]
        [InlineData(1, 0)]
        [InlineData(1, -1)]
        public void CalcularImposto_PrazoResgateMenorIgualZero_RetornarExcecao(int idInvestimento, int prazoResgate)
        {
            _mockImpostoInvestimentoService = new Mock<IImpostoInvestimentoService>();
            _mockImpostoRepository = new Mock<IImpostoRepository>();
            var listImpostoInvestimentoDtoMockValido = _mockIMapper.Map<IEnumerable<ImpostoInvestimentoDTO>>(listImpostoInvestimentoMockValido);
            _mockImpostoInvestimentoService.Setup(a => a.GetByIdInvestimento(It.Is<int>(b => b == 1))).Returns(listImpostoInvestimentoDtoMockValido);


            IImpostoService impostoService = new ImpostoService(_mockImpostoRepository.Object, _mockImpostoInvestimentoService.Object, _mockIMapper);

            var exception = Assert.Throws<Exception>(() => impostoService.CalculaImpostoLiquido(idInvestimento, prazoResgate));
            Assert.Equal("Prazo de resgate deve ser maior que zero.", exception.Message);
        }
    }
}
