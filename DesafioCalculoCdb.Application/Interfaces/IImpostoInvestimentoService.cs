using DesafioCalculoCdb.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DesafioCalculoCdb.Application.Interfaces
{
    public interface IImpostoInvestimentoService
    {
        Task<IEnumerable<ImpostoInvestimentoDTO>> GetImpostoInvestimentosAtivos();
        Task<ImpostoInvestimentoDTO> GetById(int id);
        IEnumerable<ImpostoInvestimentoDTO> GetByIdInvestimento(int idInvestimento);
    }
}
