using DesafioCalculoCdb.Domain.Entities;
using FluentAssertions;
using System;
using Xunit;

namespace DesafioCalculoCdb.Tests.DomainTests
{
    public class ImpostoTest
    {
        [Theory]
        [InlineData(1)]
        [InlineData(null)]
        public void CriarImposto_ParametrosValidos_RetornarObjetoValido(int? idImposto)
        {
            var nomeImposto = "Teste";
            var idtipoImposto = 1;
            var valor = 9.99m;
            var ativo = true;

            Action action = () => new Imposto(nomeImposto, idtipoImposto, valor, ativo, idImposto);
            action.Should()
                .NotThrow<CleanArchMvc.Domain.Validation.DomainExceptionValidation>();

            action.Target.Should().NotBeNull();
        }

        [Theory]
        [InlineData(1, "Teste", 0, 12, true)]
        [InlineData(1, "", 1, 12, true)]
        [InlineData(1, "Teste", 1, 0, false)]
        [InlineData(0, "Teste", 1, 1, false)]
        public void CriarImposto_ParametrosInvalidos_RetornarExcecao(int? idImposto, string nomeImposto, int idtipoImposto, decimal valor, bool ativo)
        {
            var dataDeCriacao = DateTime.Now;
            var dataDeFinalizacao = DateTime.Now;

            Action action = () => new Imposto(nomeImposto, idtipoImposto, valor, ativo, idImposto);
            action.Should()
                .Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>();
        }
    }
}
