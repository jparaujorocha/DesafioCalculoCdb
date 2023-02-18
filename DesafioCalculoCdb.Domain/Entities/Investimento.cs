using CleanArchMvc.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace DesafioCalculoCdb.Domain.Entities
{
    public class Investimento : EntidadeBase
    {
        public string Nome { get; private set; }
        public DateTime DataDeCriacao { get; private set; }
        public DateTime? DataDeFinalizacao { get; private set; }
        public decimal ValorTaxaInvestimento { get; private set; }
        public decimal ValorTaxaBanco { get; private set; }
        public bool Ativo { get; private set; }
        public ICollection<ImpostoInvestimento> ImpostoInvestimentos { get; set; }

        public Investimento()
        { }
        public Investimento(string nome, DateTime dataDeCriacao, DateTime? dataDeFinalizacao, decimal valorTaxaInvestimento,
                            decimal valorTaxaBanco, bool ativo, int? idInvestimento = null)
        {
            ValidarDominio(nome, dataDeCriacao, dataDeFinalizacao, valorTaxaInvestimento, valorTaxaBanco, ativo, idInvestimento);
            AtribuirValoresDominio(idInvestimento, nome, dataDeCriacao, dataDeFinalizacao, valorTaxaInvestimento, valorTaxaBanco, ativo);
        }

        [ExcludeFromCodeCoverage]
        private void ValidarDominio(string nome, DateTime dataDeCriacao, DateTime? dataDeFinalizacao, decimal valorTaxaInvestimento,
                            decimal valorTaxaBanco, bool ativo, int? idInvestimento = null)
        {
            DomainExceptionValidation.When(string.IsNullOrWhiteSpace(nome),
                "Nome do investimento é obrigatório");
            DomainExceptionValidation.When(valorTaxaInvestimento <= 0,
                "Valor da taxa do banco tem que ser maior que 0.");
            DomainExceptionValidation.When(valorTaxaBanco <= 0,
                "Valor da taxa do investimento tem que ser maior que 0.");
            DomainExceptionValidation.When(DateTime.TryParse(dataDeCriacao.ToString(), out _) == false,
                "Data de criação deve ser informada com uma data válida.");
            DomainExceptionValidation.When(idInvestimento != null && idInvestimento <= 0,
                "IdInvestimento Inválido.");
        }

        [ExcludeFromCodeCoverage]
        private void AtribuirValoresDominio(int? idInvestimento, string nome, DateTime dataDeCriacao, DateTime? dataDeFinalizacao, decimal valorTaxaInvestimento,
                            decimal valorTaxaBanco, bool ativo)
        {
            if (idInvestimento != null && idInvestimento > 0)
                Id = (int)idInvestimento;

            Nome = nome;
            DataDeCriacao = dataDeCriacao;
            DataDeFinalizacao = dataDeFinalizacao;
            ValorTaxaInvestimento = valorTaxaInvestimento;
            ValorTaxaBanco = valorTaxaBanco;
            Ativo = ativo;
        }
    }
}
