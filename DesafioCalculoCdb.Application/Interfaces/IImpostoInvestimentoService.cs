using DesafioCalculoCdb.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DesafioCalculoCdb.Application.Interfaces
{
    public interface IImpostoInvestimentoService
    {
        Task<IEnumerable<ImpostoInvestimentoDto>> GetImpostoInvestimentosAtivos();
        Task<ImpostoInvestimentoDto> GetById(int id);
        IEnumerable<ImpostoInvestimentoDto> GetByIdInvestimento(int idInvestimento);
    }
}
