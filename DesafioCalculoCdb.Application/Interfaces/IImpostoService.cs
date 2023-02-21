using DesafioCalculoCdb.Application.DTOs;
using System.Collections.Generic;

namespace DesafioCalculoCdb.Application.Interfaces
{
    public interface IImpostoService
    {
        IEnumerable<ImpostoDto> GetByIdInvestimento(int idInvestimento);
        decimal CalculaImpostoLiquido(int idInvestimento, int prazoResgate);
    }
}
