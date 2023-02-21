using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesafioCalculoCdb.Application.DTOs
{
    public class InvestimentoDto
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public int PrazoResgateAplicacao { get; set; }

        public decimal ValorInicialInvestimento { get; set; }

        public decimal ValorFinalInvestimentoLiquido { get; set; }

        public decimal ValorFinalInvestimentoBruto { get; set; }

        public decimal ValorImposto { get; set; }

        public decimal ValorTaxaBanco { get; set; }


        public decimal ValorTaxaInvestimento { get; set; }

        public IList<InvestimentoMensalDto> ListInvestimentoMensalDto { get; set; }
    }

    public class InvestimentoMensalDto
    {
        public int NumeroMes { get; set; }

        public decimal ValorInicialMensal { get; set; }

        public decimal ValorFinalMensal { get; set; }
    }
}
