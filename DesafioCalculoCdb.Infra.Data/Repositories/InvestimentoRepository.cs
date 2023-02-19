using DesafioCalculoCdb.Domain.Entities;
using DesafioCalculoCdb.Domain.Interfaces;
using DesafioCalculoCdb.Infra.Data.Context;
using Microsoft.EntityFrameworkCore.Internal;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioCalculoCdb.Infra.Data.Repositories
{
    public class InvestimentoRepository : IInvestimentoRepository
    {
        private ApplicationDbContext _investimentoContext;
        public InvestimentoRepository(ApplicationDbContext context)
        {
            _investimentoContext = context;
        }
        public async Task<Investimento> GetById(int id)
        {
            var investimento = await _investimentoContext.Investimentos.FindAsync(id);
            return investimento;
        }

        public bool VerificaExistenciaInvestimento(int idInvestimento)
        {

            return _investimentoContext.Investimentos.Any(a => a.Id == idInvestimento);
        }

        public async Task<IEnumerable<Investimento>> GetInvestimentosAtivos()
        {
            return await _investimentoContext.Investimentos.Where(a => a.Ativo).Include(b => b.ImpostoInvestimentos).ToListAsync();
        }
    }
}
