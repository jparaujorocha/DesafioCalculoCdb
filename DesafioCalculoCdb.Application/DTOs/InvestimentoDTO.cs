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

        [Required(ErrorMessage = "O nome do investimento é obrigatório.")]
        [MinLength(3)]
        [MaxLength(100)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O prazo de resgate da aplicação é obrigatório.")]
        [DisplayName("Prazo de resgate da aplicação")]
        [Range(1, 999)]
        public int PrazoResgateAplicacao { get; set; }

        [Required(ErrorMessage = "O Valor inicial do investimento é obrigatório.")]
        [Column(TypeName = "decimal(18,2)")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [DataType(DataType.Currency)]
        [DisplayName("Valor Inicial do investimento")]
        [Range(0.1, Double.MaxValue, ErrorMessage = "Valor Inicial do investimento precisa ser maior que zero.")]

        public decimal ValorInicialInvestimento { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [DisplayFormat(DataFormatString = "{0:N2}")]
        [DataType(DataType.Currency)]
        [DisplayName("Valor Final do investimento")]
        public decimal ValorFinalInvestimento { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [DataType(DataType.Currency)]
        [DisplayName("Valor do Imposto")]
        public decimal ValorImposto { get; set; }

        //[Required(ErrorMessage = "O Valor da taxa do banco é obrigatório.")]
        [Column(TypeName = "decimal(18,2)")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [DataType(DataType.Currency)]
        [DisplayName("Taxa do Banco")]
        public decimal ValorTaxaBanco { get; set; }

        //[Required(ErrorMessage = "O Valor da taxa do banco é obrigatório.")]
        [Column(TypeName = "decimal(18,2)")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [DataType(DataType.Currency)]
        [DisplayName("Valor Taxa Investimento")]
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
