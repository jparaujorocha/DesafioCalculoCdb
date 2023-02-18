using DesafioCalculoCdb.Domain.Entities;
using FluentAssertions;
using System;
using Xunit;

namespace DesafioCalculoCdb.Tests.DomainTests
{
    public class ImpostoInvestimentoTest
    {
        [Theory]
        [InlineData(1)]
        [InlineData(null)]
        public void CriarImpostoInvestimento_ParametrosValidos_RetornarObjetoValido(int? idImpostoInvestimento)
        {
            var idImposto = 1;
            var idInvestimento = 4;
            var DataInicio = DateTime.Now;
            var ativo = true;

            Action action = () => new ImpostoInvestimento(idImposto, idInvestimento, DataInicio, null,  ativo, idImpostoInvestimento);
            action.Should()
                .NotThrow<CleanArchMvc.Domain.Validation.DomainExceptionValidation>();

            action.Target.Should().NotBeNull();
        }

        [Theory]
        [InlineData(1, 1, 0, true)]
        [InlineData(0, 1, 1, true)]
        [InlineData(2, 0, 1, true)]
        [InlineData(3, 1, 0, false)]
        [InlineData(0, 1, -2, true)]
        public void CriarImpostoInvestimento_ParametrosInvalidos_RetornarExcecao(int? idImpostoInvestimento, int idImposto, int idInvestimento, bool ativo)
        {
            var DataInicio = DateTime.Now;
            var dataDeFinalizacao = DateTime.Now;

            Action action = () => new ImpostoInvestimento(idImposto, idInvestimento, DataInicio, dataDeFinalizacao, ativo, idImpostoInvestimento);
            action.Should()
                .Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>();
        }

    }
}
