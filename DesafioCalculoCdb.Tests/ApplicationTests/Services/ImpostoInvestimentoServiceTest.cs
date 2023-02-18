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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Xunit;

namespace DesafioCalculoCdb.Tests.ApplicationTests.Services
{
    public class ImpostoInvestimentoServiceTest
    {
        private Moq.Mock<IImpostoInvestimentoRepository> _mockImpostoInvestimentoRepository;
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

        private ImpostoInvestimento impostoInvestimentoMockValido = new ImpostoInvestimento(1, 1, DateTime.Now, null, true, 1);
        private IEnumerable<ImpostoInvestimento> listImpostoInvestimentoMockValido = new List<ImpostoInvestimento>
            {
                new ImpostoInvestimento(1, 1, DateTime.Now, null, true, 1 ),
                new ImpostoInvestimento(1, 1, DateTime.Now,null,true,2),
                new ImpostoInvestimento(1, 2, DateTime.Now, null, true, 3)
            };

        private ImpostoInvestimentoDTO impostoInvestimentoDtoMockValido;
        private IEnumerable<ImpostoInvestimentoDTO> listImpostoInvestimentoDtoMockValido;

        [Fact]
        public void GetImpostoInvestimentosAtivos_SemParametro_RetornaListaObjeto()
        {
            _mockImpostoInvestimentoRepository = new Mock<IImpostoInvestimentoRepository>();
            listImpostoInvestimentoDtoMockValido = _mockIMapper.Map<IEnumerable<ImpostoInvestimentoDTO>>(listImpostoInvestimentoMockValido);

            IImpostoInvestimentoService impostoInvestimentoService = new ImpostoInvestimentoService(_mockImpostoInvestimentoRepository.Object,
                                                                                                    _mockIMapper);

            _mockImpostoInvestimentoRepository.Setup(a => a.GetImpostosInvestimentosAtivos()).Returns(Task.FromResult(listImpostoInvestimentoMockValido));

            var listaImpostoInvestimento = impostoInvestimentoService.GetImpostoInvestimentosAtivos();

            listImpostoInvestimentoDtoMockValido.Should().BeEquivalentTo(listaImpostoInvestimento.Result);
        }
        [Fact]
        public void GetImpostoInvestimentosAtivos_SemParametro_RetornaListaObjetoVazia()
        {
            listImpostoInvestimentoMockValido = null;
            _mockImpostoInvestimentoRepository = new Mock<IImpostoInvestimentoRepository>();
            listImpostoInvestimentoDtoMockValido = new List<ImpostoInvestimentoDTO>();

            IImpostoInvestimentoService impostoInvestimentoService = new ImpostoInvestimentoService(_mockImpostoInvestimentoRepository.Object,
                                                                                                    _mockIMapper);
            _mockImpostoInvestimentoRepository.Setup(a => a.GetImpostosInvestimentosAtivos()).Returns(Task.FromResult(listImpostoInvestimentoMockValido));
            
            var listaImpostoInvestimento = impostoInvestimentoService.GetImpostoInvestimentosAtivos();

            listImpostoInvestimentoDtoMockValido.Should().BeEquivalentTo(listaImpostoInvestimento.Result);
        }

        [Fact]
        public void GetImpostoInvestimentoById_IdExistente_RetornaObjeto()
        {
            _mockImpostoInvestimentoRepository = new Mock<IImpostoInvestimentoRepository>();
            impostoInvestimentoDtoMockValido = _mockIMapper.Map<ImpostoInvestimentoDTO>(impostoInvestimentoMockValido);


            IImpostoInvestimentoService impostoInvestimentoService = new ImpostoInvestimentoService(_mockImpostoInvestimentoRepository.Object,
                                                                                                    _mockIMapper);

            _mockImpostoInvestimentoRepository.Setup(a => a.GetById(It.Is<int>(b => b == 1))).Returns(Task.FromResult(impostoInvestimentoMockValido));


            var impostoInvestimento = impostoInvestimentoService.GetById(1);

            impostoInvestimentoDtoMockValido.Should().BeEquivalentTo(impostoInvestimento.Result);
        }
        [Fact]
        public void GetImpostoInvestimentoById_IdNaoExistente_RetornaObjetoVazio()
        {
            _mockImpostoInvestimentoRepository = new Mock<IImpostoInvestimentoRepository>();
            impostoInvestimentoDtoMockValido = _mockIMapper.Map<ImpostoInvestimentoDTO>(impostoInvestimentoMockValido);


            IImpostoInvestimentoService impostoInvestimentoService = new ImpostoInvestimentoService(_mockImpostoInvestimentoRepository.Object,
                                                                                                    _mockIMapper);

            _mockImpostoInvestimentoRepository.Setup(a => a.GetById(It.Is<int>(b => b == 1))).Returns(Task.FromResult(impostoInvestimentoMockValido));


            var impostoInvestimento = impostoInvestimentoService.GetById(0);

            impostoInvestimento.Result.Should().BeNull();
        }

        [Fact]
        public void GetImpostoInvestimentoByIdInvestimento_IdInvestimentoExistente_RetornaListaObjeto()
        {
            _mockImpostoInvestimentoRepository = new Mock<IImpostoInvestimentoRepository>();
            listImpostoInvestimentoDtoMockValido = _mockIMapper.Map<IEnumerable<ImpostoInvestimentoDTO>>(listImpostoInvestimentoMockValido);
            listImpostoInvestimentoDtoMockValido = listImpostoInvestimentoDtoMockValido.Where(a => a.IdInvestimento == 2).Select(b => b);

            IImpostoInvestimentoService impostoInvestimentoService = new ImpostoInvestimentoService(_mockImpostoInvestimentoRepository.Object,
                                                                                                    _mockIMapper);

            _mockImpostoInvestimentoRepository.Setup(a => a.GetByIdInvestimento(It.Is<int>(b => b == 2))).Returns(listImpostoInvestimentoMockValido.Where(a => a.IdInvestimento == 2));

            var listaImpostoInvestimento = impostoInvestimentoService.GetByIdInvestimento(2);

            listImpostoInvestimentoDtoMockValido.Should().BeEquivalentTo(listaImpostoInvestimento);
        }

        [Fact]
        public void GetImpostoInvestimentoByIdInvestimento_IdNaoExistente_RetornaListaObjetoVazia()
        {
            listImpostoInvestimentoMockValido = null;
            _mockImpostoInvestimentoRepository = new Mock<IImpostoInvestimentoRepository>();
            listImpostoInvestimentoDtoMockValido = new List<ImpostoInvestimentoDTO>();

            IImpostoInvestimentoService impostoInvestimentoService = new ImpostoInvestimentoService(_mockImpostoInvestimentoRepository.Object,
                                                                                                    _mockIMapper);

            _mockImpostoInvestimentoRepository.Setup(a => a.GetByIdInvestimento(It.Is<int>(b => b >= 1 && b <= 2))).Returns(listImpostoInvestimentoMockValido);

            var listaImpostoInvestimento = impostoInvestimentoService.GetByIdInvestimento(6);

            listImpostoInvestimentoDtoMockValido.Should().BeEquivalentTo(listaImpostoInvestimento);
        }
    }
}
