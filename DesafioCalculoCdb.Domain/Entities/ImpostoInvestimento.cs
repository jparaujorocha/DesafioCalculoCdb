using CleanArchMvc.Domain.Validation;
using System;
using System.Diagnostics.CodeAnalysis;

namespace DesafioCalculoCdb.Domain.Entities
{
    public class ImpostoInvestimento : EntidadeBase
    {
        public int IdImposto { get; set; }

        public int IdInvestimento { get; set; }

        public DateTime DataInicio { get; private set; }

        public DateTime? DataFim { get; private set; }

        public bool Ativo { get; private set; }
        public Investimento Investimento { get; set; }
        public Imposto Imposto { get; set; }

        public ImpostoInvestimento()
        {

        }
        public ImpostoInvestimento(int idImposto, int idInvestimento, DateTime dataInicio, DateTime? dataFim, bool ativo, int? idImpostoInvestimento = null )
        {
            ValidarDominio(idImposto, idInvestimento, dataInicio, dataFim, ativo, idImpostoInvestimento);
            AtribuirValoresDominio(idImpostoInvestimento, idImposto, idInvestimento, dataInicio, dataFim, ativo);
        }

        [ExcludeFromCodeCoverage]
        private void ValidarDominio(int idImposto, int idInvestimento, DateTime dataInicio, DateTime? dataFim, bool ativo,
                                    int? idImpostoInvestimento = null)
        {
            DomainExceptionValidation.When(idImposto <= 0,
                "IdImposto deve ser um valor válido.");
            DomainExceptionValidation.When(idInvestimento <= 0,
                "IdInvestimento deve ser um valor válido.");
            DomainExceptionValidation.When(DateTime.TryParse(dataInicio.ToString(), out _) == false,
                "Data de criação deve ser informada com uma data válida.");
            DomainExceptionValidation.When(idImpostoInvestimento != null && idImpostoInvestimento <= 0,
                "idImpostoInvestimento Inválido.");
        }

        [ExcludeFromCodeCoverage]
        private void AtribuirValoresDominio(int? idImpostoInvestimento, int idImposto, int idInvestimento, DateTime dataInicio, DateTime? dataFim, bool ativo)
        {
            if (idImpostoInvestimento.HasValue && idImpostoInvestimento > 0)
                Id = idImpostoInvestimento.Value;

            IdImposto = idImposto;
            IdInvestimento = idInvestimento;
            DataInicio = dataInicio;
            DataFim = dataFim;
            Ativo = ativo;
        }
    }
}
