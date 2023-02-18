using DesafioCalculoCdb.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DesafioCalculoCdb.Domain.Interfaces
{
    public interface IImpostoInvestimentoRepository
    {
        Task<IEnumerable<ImpostoInvestimento>> GetImpostosInvestimentosAtivos();
        Task<ImpostoInvestimento> GetById(int id);
        IEnumerable<ImpostoInvestimento> GetByIdInvestimento(int idInvestimento);
    }
}
