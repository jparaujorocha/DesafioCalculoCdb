using DesafioCalculoCdb.Domain.Entities;
using FluentAssertions;
using System;
using Xunit;

namespace DesafioCalculoCdb.Tests.DomainTests
{
    public class InvestimentoTest
    {
        [Theory]
        [InlineData(1)]
        [InlineData(null)]
        public void CriarInvestimento_ParametrosValidos_RetornarObjetoValido(int? idInvestimento)
        {
            var nomeInvestimento = "Teste";
            var dataDeCriacao = DateTime.Now;
            var dataDeFinalizacao = DateTime.Now;
            var valorTaxaInvestimento = 9.99m;
            var valorTaxaBanco = 99;
            var ativo = true;

            Action action = () => new Investimento(nomeInvestimento, dataDeCriacao, null, valorTaxaInvestimento,
                valorTaxaBanco, ativo, idInvestimento);
            action.Should()
                .NotThrow<CleanArchMvc.Domain.Validation.DomainExceptionValidation>();

            action.Target.Should().NotBeNull();
        }

        [Theory]
        [InlineData(1, "Teste", 0, 12, true)]
        [InlineData(1, "", 13.02, 12, true)]
        [InlineData(1, "Teste", 14.14, -5, false)]
        [InlineData(-8, "Teste", 13, 12, true)]
        [InlineData(0, "Teste", 12, 2.45, true)]
        public void CriarInvestimento_ParametrosInvalidos_RetornarExcecao(int? idInvestimento, string nomeInvestimento, decimal valorTaxaInvestimento,
                                                                         decimal valorTaxaBanco, bool ativo)
        {
            var dataDeCriacao = DateTime.Now;
            var dataDeFinalizacao = DateTime.Now;

            Action action = () => new Investimento(nomeInvestimento, dataDeCriacao, null, valorTaxaInvestimento,
                valorTaxaBanco, ativo, idInvestimento);
            action.Should()
                .Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>();
        }
    }
}
