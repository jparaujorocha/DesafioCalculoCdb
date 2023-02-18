using DesafioCalculoCdb.Domain.Entities;
using FluentAssertions;
using System;
using Xunit;

namespace DesafioCalculoCdb.Tests.DomainTests
{
    public class TipoImpostoTest
    {
        [Theory]
        [InlineData(1)]
        [InlineData(null)]
        public void CriarTipoImposto_ParametrosValidos_RetornarObjetoValido(int? idTipoImposto)
        {
            var nomeTipoImposto = "Teste";

            Action action = () => new TipoImposto(nomeTipoImposto, idTipoImposto);
            action.Should()
                .NotThrow<CleanArchMvc.Domain.Validation.DomainExceptionValidation>();

            action.Target.Should().NotBeNull();
        }

        [Theory]
        [InlineData(0, "Teste")]
        [InlineData(1, "")]
        public void CriarTipoImposto_ParametrosInvalidos_RetornarExcecao(int? idTipoImposto, string nomeTipoImposto)
        {
            Action action = () => new TipoImposto(nomeTipoImposto, idTipoImposto);
            action.Should()
                .Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>();
        }
    }
}
