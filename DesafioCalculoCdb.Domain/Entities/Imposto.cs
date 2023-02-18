using CleanArchMvc.Domain.Validation;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace DesafioCalculoCdb.Domain.Entities
{
    public class Imposto : EntidadeBase
    {
        public string Nome { get; private set; }
        public int IdTipoImposto { get; private set; }
        public decimal Valor { get; private set; }
        public bool Ativo { get; private set; }        
        public TipoImposto TipoImposto { get; set; }
        public ICollection<ImpostoInvestimento> ImpostoInvestimentos { get; set; }

        public Imposto()
        {

        }

        public Imposto(string nome, int idTipoImposto, decimal valor, bool ativo, int? idImposto = null)
        {
            ValidarDominio(nome, idTipoImposto, valor, ativo, idImposto);
            AtribuirValoresDominio(idImposto, nome, idTipoImposto, valor, ativo);
        }

        [ExcludeFromCodeCoverage]
        private void ValidarDominio(string nome, int idTipoImposto, decimal valor, bool ativo, int? idImposto = null)
        {
            DomainExceptionValidation.When(string.IsNullOrWhiteSpace(nome),
                "Nome do investimento é obrigatório");
            DomainExceptionValidation.When(valor <= 0,
                "Valor do imposto tem que ser maior que 0.");
            DomainExceptionValidation.When(idTipoImposto <= 0,
                "Tipo do Imposto inválido.");
            DomainExceptionValidation.When(idImposto != null && idImposto <= 0,
                "IdImposto Inválido.");
        }
        [ExcludeFromCodeCoverage]
        private void AtribuirValoresDominio(int? idImposto, string nome, int idTipoImposto, decimal valor, bool ativo)
        {
            if (idImposto.HasValue && idImposto.Value > 0)
                Id = idImposto.Value;

            Nome = nome;
            IdTipoImposto = idTipoImposto;
            Valor = valor;
            Ativo = ativo;
        }
    }
}
