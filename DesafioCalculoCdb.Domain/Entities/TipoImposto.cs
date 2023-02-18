using CleanArchMvc.Domain.Validation;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace DesafioCalculoCdb.Domain.Entities
{
    public class TipoImposto : EntidadeBase
    {
        public string Nome { get; private set; }
        public ICollection<Imposto> Impostos { get; set; }

        public TipoImposto()
        { }
        public TipoImposto(string nome, int? idTipoImposto = null)
        {
            ValidarDominio(nome, idTipoImposto);
            AtribuirValoresDominio(idTipoImposto, nome);
        }
        [ExcludeFromCodeCoverage]
        private void ValidarDominio(string nome, int? idTipoImposto = null)
        {
            DomainExceptionValidation.When(string.IsNullOrWhiteSpace(nome),
                "Nome é obrigatório");

            DomainExceptionValidation.When(idTipoImposto != null && idTipoImposto <= 0,
                "IdTipoImposto Inválido.");
        }
        [ExcludeFromCodeCoverage]
        private void AtribuirValoresDominio(int? idTipoImposto, string nome)
        {
            if (idTipoImposto.HasValue && idTipoImposto.Value > 0)
                Id = idTipoImposto.Value;

            Nome = nome;
        }
    }
}
