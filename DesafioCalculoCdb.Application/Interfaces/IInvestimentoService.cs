using DesafioCalculoCdb.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DesafioCalculoCdb.Application.Interfaces
{
    public interface IInvestimentoService
    {
        Task<IEnumerable<InvestimentoDTO>> GetInvestimentosAtivos();
        Task<InvestimentoDTO> GetById(int id);
        Task<InvestimentoDTO> CalculaSimulacaoInvestimentos(InvestimentoDTO dadosInvestimento);
    }
}
