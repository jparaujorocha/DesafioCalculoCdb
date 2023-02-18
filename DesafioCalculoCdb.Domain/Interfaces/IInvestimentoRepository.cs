using DesafioCalculoCdb.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DesafioCalculoCdb.Domain.Interfaces
{
    public interface IInvestimentoRepository
    {
        Task<IEnumerable<Investimento>> GetInvestimentosAtivos();
        Task<Investimento> GetById(int id);
    }
}
