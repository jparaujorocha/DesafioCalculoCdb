using System;

namespace DesafioCalculoCdb.Application.DTOs
{
    public class ImpostoInvestimentoDto
    {
        public int Id { get; set; }

        public int IdImposto { get; set; }

        public int IdInvestimento { get; set; }

        public DateTime DataInicio { get;  set; }

        public DateTime? DataFim { get;  set; }

        public bool Ativo { get;  set; }
    }
}
