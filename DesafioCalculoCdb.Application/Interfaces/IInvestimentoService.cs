using DesafioCalculoCdb.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DesafioCalculoCdb.Application.Interfaces
{
    public interface IInvestimentoService
    {
        Task<IEnumerable<InvestimentoDto>> GetInvestimentosAtivos();
        Task<InvestimentoDto> GetById(int id);
        Task CalculaSimulacaoInvestimentos(InvestimentoDto dadosInvestimento);
        bool VerificaExistenciaDeInvestimento(int idInvestimento);
    }
}
